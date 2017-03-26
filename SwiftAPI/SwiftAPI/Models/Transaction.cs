using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace SwiftAPI.Models
{
    public class Transaction
    {
        public DateTime dateTime { get; set; }
        public Debtor debtor { get; set; }
        public Creditor creditor { get; set; }
        public string currency { get; set; }
        public decimal amount { get; set; }
        public string EndToEnd { get; set; }
        public string invoice { get; set; }
        public Boolean validSchema { get; set; }
        public List<transactionError> errorList { get; set; }
    }

    public class Debtor
    {
        public string name { get; set; }
        //public string country { get; set; }
        public string IBAN { get; set; }
        public string bankName { get; set; }

    }

    public class Creditor
    {
        public string name { get; set; }
        public string country { get; set; }
        public string IBAN { get; set; }
        public string bankName { get; set; }
        public string address { get; set; }
        //public string city { get; set; }
        //public string zipcode { get; set; }
    }

    public static class transactionConstructor
    {
        public static Transaction Parse(XDocument xml)
        {
            Transaction transaction = new Transaction();
            Debtor debtor = new Debtor();
            Creditor creditor = new Creditor();
            XmlReader reader = xml.CreateReader();


            XmlDocument doc = new XmlDocument();
            string xmls = xml.ToString();
            doc.LoadXml(xmls);
            string json = Newtonsoft.Json.JsonConvert.SerializeXmlNode(doc);

            var parsed = JObject.Parse(json);

            transaction.dateTime = parsed.SelectToken("Document.CstmrCdtTrfInitn.GrpHdr.CreDtTm").Value<DateTime>();
            JToken t = parsed.SelectToken("Document.CstmrCdtTrfInitn.PmtInf.CdtTrfTxInf.Amt.InstdAmt");
            transaction.currency = t.First.First.Value<string>();
            transaction.amount = t.Last.First.Value<decimal>();
            transaction.EndToEnd = parsed.SelectToken("Document.CstmrCdtTrfInitn.PmtInf.PmtInfId").Value<string>();
            transaction.invoice = parsed.SelectToken("Document.CstmrCdtTrfInitn.PmtInf.CdtTrfTxInf.RmtInf.Ustrd").Value<string>();

            debtor.name = parsed.SelectToken("Document.CstmrCdtTrfInitn.PmtInf.Dbtr.Nm").Value<string>();
            debtor.IBAN = parsed.SelectToken("Document.CstmrCdtTrfInitn.PmtInf.DbtrAcct.Id.IBAN").Value<string>();
            debtor.bankName = parsed.SelectToken("Document.CstmrCdtTrfInitn.PmtInf.DbtrAgt.FinInstnId.BIC").Value<string>();
            //debtor.country = parsed.SelectToken("Document.CstmrCdtTrfInitn.PmtInf.


            creditor.name = parsed.SelectToken("Document.CstmrCdtTrfInitn.PmtInf.CdtTrfTxInf.Cdtr.Nm").Value<string>();
            creditor.IBAN = parsed.SelectToken("Document.CstmrCdtTrfInitn.PmtInf.CdtTrfTxInf.CdtrAcct.Id.IBAN").Value<string>();
            creditor.bankName = parsed.SelectToken("Document.CstmrCdtTrfInitn.PmtInf.CdtTrfTxInf.CdtrAgt.FinInstnId.BIC").Value<string>();
            creditor.country = parsed.SelectToken("Document.CstmrCdtTrfInitn.PmtInf.CdtTrfTxInf.Cdtr.PstlAdr.Ctry").Value<string>();
            creditor.address = parsed.SelectToken("Document.CstmrCdtTrfInitn.PmtInf.CdtTrfTxInf.Cdtr.PstlAdr.AdrLine").First.Value<string>() + parsed.SelectToken("Document.CstmrCdtTrfInitn.PmtInf.CdtTrfTxInf.Cdtr.PstlAdr.AdrLine").Last.Value<string>();
            creditor.IBAN = parsed.SelectToken("Document.CstmrCdtTrfInitn.PmtInf.CdtTrfTxInf.CdtrAcct.Id.IBAN").Value<string>();


            transaction.creditor = creditor;
            transaction.debtor = debtor;

            return transaction;
        }


    }




}