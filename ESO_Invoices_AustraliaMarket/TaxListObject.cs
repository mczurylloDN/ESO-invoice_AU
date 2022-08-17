using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESO_InvoicesDemo
{
    public class TaxListObject
    {
        private string ExternalId;
        private string TaxAmount;


        public TaxListObject(string _ExternalId, string _TaxAmount)
        {
            ExternalId = _ExternalId;
            TaxAmount = _TaxAmount;

        }

        public string ExternalId_p
        {
            get { return ExternalId; }
            set { ExternalId = value; }
        }

        public string TaxAmount_p
        {
            get { return TaxAmount; }
            set { TaxAmount = value; }
        }

    }
}
