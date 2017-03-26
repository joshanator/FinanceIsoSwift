using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Schema;

namespace FinanceIsoSwiftAPI.Models
{
    public class XMLSchemaValidation
    {

        public void ValidateXmlDocument(string xml, string schemaPath)
        {
            XmlReader documentToValidate = XmlReader.Create(new StringReader(xml));

            XmlSchema schema;
            using (var schemaReader = XmlReader.Create(schemaPath))
            {
                schema = XmlSchema.Read(schemaReader, ValidationEventHandler);
            }

            var schemas = new XmlSchemaSet();
            schemas.Add(schema);

            var settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas = schemas;
            settings.ValidationFlags =
                XmlSchemaValidationFlags.ProcessIdentityConstraints |
                XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.ValidationEventHandler += ValidationEventHandler;

            using (var validationReader = XmlReader.Create(documentToValidate, settings))
            {
                while (validationReader.Read())
                {
                }
            }
        }

        private static void ValidationEventHandler(
            object sender, ValidationEventArgs args)
        {
            if (args.Severity == XmlSeverityType.Error)
            {
                throw args.Exception;
            }

            Debug.WriteLine(args.Message);
        }


    }
}