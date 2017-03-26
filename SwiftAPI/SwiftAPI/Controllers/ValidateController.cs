using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;
using System.Xml.Schema;

namespace SwiftAPI.Controllers
{
    public class ValidateController : ApiController
    {
        public Models.Transaction Get (string xml, int x)
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

        public Models.Transaction Get (string xml, string xsd)
        {
            xml = "http://financeisoswift.azurewebsites.net/uploads/" + xml + ".xml";
            XDocument doc = XDocument.Load(xml);
            string xmlFile = doc.ToString();

            Models.Transaction transaction = Models.transactionConstructor.Parse(doc);

            string nSpace = "http://financeisoswift.azurewebsites.net/uploads/" + xsd;
           
            xsd = nSpace + ".xsd";


            bool hasErrors = false;
            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add(null, xsd);

            XDocument doc1 = XDocument.Load(xml);

            doc1.Validate(schemas, (o, vea) =>
            {
                Console.WriteLine(o.GetType().Name);
                Console.WriteLine(vea.Message);

                hasErrors = true;
            }, true);

            if (hasErrors)
            {
                transaction.validSchema = false;
            }
            else
            {
                transaction.validSchema = true;
            }

            List<Models.transactionError> errorList = Models.transactionErrorConstructor.Validate(transaction);



            transaction.errorList = errorList;


            return transaction;
        }


    }
}
