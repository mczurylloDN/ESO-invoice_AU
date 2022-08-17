using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESO_InvoicesDemo
{
    public class ResultsObject
    {
        private string FileName;
        private string Errors;
        private string InvoiceNumber;


        public ResultsObject(string _InvoiceNumber , string _FileName, string _Errors)
        {
            FileName = _FileName;
            Errors = _Errors;
            InvoiceNumber = _InvoiceNumber;

        }

        public string FileName_p
        {
            get { return FileName; }
            set { FileName = value; }
        }

        public string InvoiceNumber_p
        {
            get { return InvoiceNumber; }
            set { InvoiceNumber = value; }
        }

        public string Errors_p
        {
            get { return Errors; }
            set { Errors = value; }
        }
    }
}
