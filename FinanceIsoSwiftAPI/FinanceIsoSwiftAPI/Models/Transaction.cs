using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinanceIsoSwiftAPI.Models
{
    public class Transaction
    {
        public DateTime dateTime { get; set; }
        public Debtor debtor { get; set; }
        public Creditor creditor { get; set; }
        public string currency { get; set; }

    }

    public class Debtor
    {
        public string name { get; set; }
        public string country { get; set; }
        public string IBAN { get; set; }
        public string bankName { get; set; }

    }

    public class Creditor
    {
        public string name { get; set; }
        public string country { get; set; }
        public string IBAN { get; set; }
        public string bankName { get; set; }
        public string street { get; set; }
        public string addressNum { get; set; }
        public string city { get; set; }
        public string zipcode { get; set; }
    }

}