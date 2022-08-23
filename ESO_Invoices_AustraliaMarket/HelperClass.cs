using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ESO_InvoicesDemo
{
    public static class HelperClass
    {

        /// <summary>
        /// READ XML  FOR AUSTRALIAN MARKET ONLY
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static ESOInvoiceSuperObject ReviewFile(string path)
        {
            //with the path we build the dynamic object XML file in memory stream of the ESO Invoice
            //this object can be manipulated in runtime. 
            XmlDocument doc = new XmlDocument();
            XmlNode NODE;
            doc.Load(path);

            //Example 1 Extract data from the File here we will extract invoices date and number 
            //	<Invoice InvoiceDate="20220708" InvoiceNumber="2193752446"> 
            string InvoiceNumber = "";
            string InvoiceDATE = "";

            //explanation DOC = the whole XML , document element places the virtual cursor on the top
            //child nodes points to = invoice list 
            //the second child node points to = invoice as object 
            //with this we can extract the first header data as it follows:
	        
		    NODE = doc.DocumentElement.ChildNodes[0].ChildNodes[0]; 
            InvoiceNumber = NODE.Attributes["InvoiceNumber"].InnerText;
            InvoiceDATE = NODE.Attributes["InvoiceDate"].InnerText;


          

            //item List Data 
            //Every Document needs to be "interated" they are lists of data to be search it
            //with this structure of ESO Invoices we need to point the cursor to the index = 2,
            //index = 2 are the ITEM LIST on the xml, and here we are going to extract just the item GST data
            //here a guide of what is contained in each NODE.CHILDNODE :
            //This is the full method to read the whole XML FILE

            ESOInvoiceObject_AU InvoiceObj;
            List<ESOInvoiceObject_AU> items_Filtered = new List<ESOInvoiceObject_AU>();
            //Child node = 2 = item gst 
            foreach (XmlNode node in NODE.ChildNodes[3].ChildNodes)
            {
                //we read first the whole ITEM GST 
                InvoiceObj = new ESOInvoiceObject_AU();
                InvoiceObj.CostExclTax_p = node.Attributes["CostExclTax"].InnerText;
                InvoiceObj.CostInclTax_p = node.Attributes["CostInclTax"].InnerText;
                InvoiceObj.ExtCostExclTax_p = node.Attributes["ExtCostExclTax"].InnerText;
                InvoiceObj.ExtCostInclTax_p = node.Attributes["ExtCostInclTax"].InnerText;
                InvoiceObj.ExtTaxAmt_p = node.Attributes["ExtTaxAmt"].InnerText;
                InvoiceObj.Name_p = node.Attributes["Name"].InnerText;
                InvoiceObj.ProductCode_p = node.Attributes["ProductCode"].InnerText;
                InvoiceObj.Quantity_p = node.Attributes["Quantity"].InnerText;
                InvoiceObj.Barcode_p = node.Attributes["Barcode"].InnerText;
                InvoiceObj.TaxID_p = node.Attributes["TaxID"].InnerText;
                InvoiceObj.PurchaseOrder_p = "";
                InvoiceObj.SSCCCode_p = ""; 
                items_Filtered.Add(InvoiceObj);
            }

            //Child node = 4 is the tax list on the xml 
            TaxListObject taxlist;
            List<TaxListObject> items_Filtered_taxlist = new List<TaxListObject>();
            foreach (XmlNode node in NODE.ChildNodes[4].ChildNodes)
            {
                taxlist = new TaxListObject(node.Attributes["ExternalId"].InnerText , node.Attributes["TaxAmount"].InnerText);
                //we place the taxlist object as well
                items_Filtered_taxlist.Add(taxlist);
            }
           
            string CostExclTax = NODE.ChildNodes[5].Attributes["CostExclTax"].InnerText;
            string CostInclTax = NODE.ChildNodes[5].Attributes["CostInclTax"].InnerText;
            string TaxAmt = NODE.ChildNodes[5].Attributes["TaxAmt"].InnerText;


           ESOInvoiceSuperObject superObject = new ESOInvoiceSuperObject(InvoiceNumber, InvoiceDATE,  items_Filtered,items_Filtered_taxlist, CostExclTax, CostInclTax, TaxAmt, path);

           //return the whole xml file in Memory to be checked out
            return superObject;
        }

       


        /// <summary>
        /// Checks if the item name is too long
        /// </summary>
        /// <param name="ListofSuperObject"></param>
        /// <returns></returns>
        public static List<ResultsObject> CheckInvoicesforItemLongName(List<ESOInvoiceSuperObject> ListofSuperObject)
        {
            string errors;
            int i = 0;
            ResultsObject objectResults;
            List<ResultsObject> ListOfErrors = new List<ResultsObject>();
            //First iteration invoice peer invoice 
            foreach (var INVOICE in ListofSuperObject)
            {
                foreach (var itemGST in INVOICE.InvoiceObject_p)
                {
                    //Check if item name is tooo long 
                    if (itemGST.Name_p.Length > 50)
                    {
                        errors = "Name of item too long :   " + itemGST.Name_p.ToString();
                       
                        objectResults = new ResultsObject(INVOICE.InvoiceNumber_p, Path.GetFileName(INVOICE.FilenamePath_p), errors);
                        ListOfErrors.Add(objectResults);
                    }
                    else if (itemGST.Name_p.Length<=50)
                    { 
                        errors = "All item names are correct";
                       
                        objectResults = new ResultsObject(INVOICE.InvoiceNumber_p, Path.GetFileName(INVOICE.FilenamePath_p), errors);
                        ListOfErrors.Add(objectResults);
                    }
               }


            }

            return ListOfErrors;
        }


        /// <summary>
        /// This method finds a duplicate barcode on the invoice 
        /// </summary>
        /// <param name="ListofSuperObject"></param>
        /// <returns></returns>
        public static List<ResultsObject> FindDuplicateItem(List<ESOInvoiceSuperObject> ListofSuperObject)
        {
          
            ResultsObject objectResults;
            List<ResultsObject> ListOfErrors = new List<ResultsObject>();

            //First iteration invoice peer invoice 
            foreach (var INVOICE in ListofSuperObject)
            {
                foreach (var itemGST in INVOICE.InvoiceObject_p)
                {
                    var dups = INVOICE.InvoiceObject_p.GroupBy(x => x.ProductCode_p)
                        .Where(x => x.Count() > 1)
                        .Select(x => x.Key)
                        .ToList();

                    if (dups.Count > 0)
                    {
                        var error = "ProductCode duplicated - Product CODE:  " + dups[0].ToString();
                        objectResults = new ResultsObject(INVOICE.InvoiceNumber_p, Path.GetFileName(INVOICE.FilenamePath_p), error);
                        ListOfErrors.Add(objectResults);
                        break;
                    }
                    else if (dups.Count == 0)
                    { 
                        var error = "There is no duplicated code";
                        objectResults = new ResultsObject(INVOICE.InvoiceNumber_p, Path.GetFileName(INVOICE.FilenamePath_p), error);
                        if(!objectResults.Contains(objectResults.InvoiceNumber_p))
                            {
                                ListOfErrors.Add(objectResults);
                            }
                        break;
                    }
                }
            }
            return ListOfErrors.Distinct().ToList();
        }





        /// <summary>
        /// Fix Aral aktion issue
        /// </summary>
        /// <param name="path"></param>
        /// <param name="Value"></param>
        public static void ReplaceValueAralAktion(List<ESOInvoiceSuperObject> ListofSuperObject)
        {
            //we set the invoice with the new path marking it as MOD to modified 
            //how this works? : we take the same logic to read the file one more time
            //we navigate on all previus invoices.
            foreach (var item in ListofSuperObject)
            {

                XmlDocument doc = new XmlDocument();
		        XmlNode NODE;
                doc.Load(item.FilenamePath_p);
                NODE = doc.DocumentElement.ChildNodes[0].ChildNodes[0];
                //we navigate again on the nodes
                foreach (XmlNode node in NODE.ChildNodes[2].ChildNodes)
                {
                     string valuetocompare = node.ChildNodes[0].ChildNodes[0].Attributes["PurchaseOrderNumber"].InnerText;
                     if(valuetocompare == "Aral_Aktion_Juli_22")
                       {
                        node.ChildNodes[0].ChildNodes[0].Attributes["PurchaseOrderNumber"].InnerText = "";
                       }
                }
                string new_path = item.FilenamePath_p.Replace(".xml", "_MOD.xml");
                doc.Save(new_path);
            }

        }


    }
}
