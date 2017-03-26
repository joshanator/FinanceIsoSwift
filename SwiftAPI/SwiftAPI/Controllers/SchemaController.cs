using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace SwiftAPI.Controllers
{
    public class SchemaController : ApiController
    {
        public Boolean Get(string xml, string xsd)
        {
            string nSpace = "http://financeisoswift.azurewebsites.net/uploads/" + xsd;
            xml = "http://financeisoswift.azurewebsites.net/uploads/" + xml + ".xml";
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
                return false;
            }

            return true;
        }
    }
}
