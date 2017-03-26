using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;

namespace SwiftAPI.Controllers
{
    public class ValidateController : ApiController
    {
        public Models.Transaction Get (string xml, string x)
        {
            xml = "http://financeisoswift.azurewebsites.net/uploads/" + xml + ".xml";
            XDocument doc = XDocument.Load(xml);
            string xmlFile = doc.ToString();

            Models.Transaction transaction = Models.transactionConstructor.Parse(doc);

            return transaction;
        }

        public List<Models.transactionError> Get(string xml)
        {
            xml = "http://financeisoswift.azurewebsites.net/uploads/" + xml + ".xml";
            XDocument doc = XDocument.Load(xml);
            string xmlFile = doc.ToString();

            Models.Transaction transaction = Models.transactionConstructor.Parse(doc);

            List<Models.transactionError> errorList = Models.transactionErrorConstructor.Validate(transaction);

            

            return errorList;

        }


    }
}
