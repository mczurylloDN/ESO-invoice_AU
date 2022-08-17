using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESO_InvoicesDemo
{
    public  class ESOInvoiceSuperObject
    {

        private List<ESOInvoiceObject_AU> InvoiceObject;
        private List<TaxListObject> TaxList;
        private string CostExclTaxTotalGST;
        private string CostInclTaxTotalGST;
        private string TaxAmtTotalGST;
        private string FilenamePath;
        private string InvoiceNumber;
        private string InvoiceDATE;


        public ESOInvoiceSuperObject(string _InvoiceNumber, string _InvoiceDate, List<ESOInvoiceObject_AU> _Invoice, List<TaxListObject> _Tax, string _CostExclTaxTotalGST,
            string _CostInclTaxTotalGST, string _TaxAmtTotalGST, string filenamePath)
        {
            InvoiceObject = _Invoice;
            TaxList = _Tax;
            CostExclTaxTotalGST = _CostExclTaxTotalGST;
            CostInclTaxTotalGST = _CostInclTaxTotalGST;
            TaxAmtTotalGST = _TaxAmtTotalGST;
            FilenamePath = filenamePath;
            InvoiceNumber = _InvoiceNumber;
            InvoiceDATE = _InvoiceDate;

        }

        public string InvoiceNumber_p
        {
            get { return InvoiceNumber; }
            set { InvoiceNumber = value; }
        }
        public string InvoiceDATE_p
        {
            get { return InvoiceDATE; }
            set { InvoiceDATE = value; }
        }
        public string FilenamePath_p
        {
            get { return FilenamePath; }
            set { FilenamePath = value; }
        }
        public string CostExclTaxTotalGST_p
        {
            get { return CostExclTaxTotalGST; }
            set { CostExclTaxTotalGST = value; }
        }

        public string CostInclTaxTotalGST_p
        {
            get { return CostInclTaxTotalGST; }
            set { CostInclTaxTotalGST = value; }
        }


        public string TaxAmtTotalGST_p
        {
            get { return TaxAmtTotalGST; }
            set { TaxAmtTotalGST = value; }
        }


        public List<ESOInvoiceObject_AU> InvoiceObject_p
        {
            get { return InvoiceObject; }
            set { InvoiceObject = value; }
        }

        public List<TaxListObject> TaxList_p
        {
            get { return TaxList; }
            set { TaxList = value; }
        }
    }
}
