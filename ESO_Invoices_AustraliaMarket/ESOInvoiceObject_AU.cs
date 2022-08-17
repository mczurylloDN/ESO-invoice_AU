using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESO_InvoicesDemo
{
    public class ESOInvoiceObject_AU
    {
        private string CostExclTax;
        private string CostInclTax;
        private string ExtCostExclTax;
        private string ExtCostInclTax;
        private string ExtTaxAmt;
        private string Name;
        private string ProductCode;
        private string Quantity;
        private string Barcode;
        private string TaxID;
        private string PurchaseOrder;
        private string SSCCCode;


        public ESOInvoiceObject_AU()
        {

        }
        public ESOInvoiceObject_AU(string _CostExclTax, string _CostInclTax, string _ExtCostExclTax, string _ExtCostInclTax, string _ExtTaxAmt,
            string _Name, string _ProductCode, string _Quantity, string _Barcode, string _TaxID,string _PurchaseOrder, string _SSCCCode)
        {
            CostExclTax = _CostExclTax;
            CostInclTax = _CostInclTax;
            ExtCostExclTax = _ExtCostExclTax;
            ExtCostInclTax = _ExtCostInclTax;
            ExtTaxAmt = _ExtTaxAmt;
            Name = _Name;
            ProductCode = _ProductCode;
            Quantity = _Quantity;
            Barcode = _Barcode;
            TaxID = _TaxID;

    }
        public string PurchaseOrder_p
        {
            get { return PurchaseOrder; }
            set { PurchaseOrder = value; }
        }
        public string SSCCCode_p
        {
            get { return SSCCCode; }
            set { SSCCCode = value; }
        }
        public string CostExclTax_p
        {
            get { return CostExclTax; }
            set { CostExclTax = value; }
        }

        public string CostInclTax_p
        {
            get { return CostInclTax; }
            set { CostInclTax = value; }
        }
        public string ExtCostExclTax_p
        {
            get { return ExtCostExclTax; }
            set { ExtCostExclTax = value; }
        }

        public string ExtCostInclTax_p
        {
            get { return ExtCostInclTax; }
            set { ExtCostInclTax = value; }
        }

        public string Name_p
        {
            get { return Name; }
            set { Name = value; }
        }
        public string ExtTaxAmt_p
        {
            get { return ExtTaxAmt; }
            set { ExtTaxAmt = value; }
        }

        public string ProductCode_p
        {
            get { return ProductCode; }
            set { ProductCode = value; }
        }
        public string Quantity_p
        {
            get { return Quantity; }
            set { Quantity = value; }
        }

        public string Barcode_p
        {
            get { return Barcode; }
            set { Barcode = value; }
        }
        public string TaxID_p
        {
            get { return TaxID; }
            set { TaxID = value; }
        }
     
    }
}
