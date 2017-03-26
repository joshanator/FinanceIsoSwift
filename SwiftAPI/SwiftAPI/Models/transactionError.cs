using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SwiftAPI.Models
{
    public class transactionError
    {
        public string section { get; set; }
        public string message { get; set; }
    }


    public static class transactionErrorConstructor
    {
        public static List<transactionError> Validate(Models.Transaction transaction)
        {
            List<transactionError> errorList = new List<transactionError>();
            List<transactionError> fErrorList = new List<transactionError>();

            errorList.Add(validateCountry(transaction.creditor.country));
            errorList.Add(validateCurrency(transaction.currency));


            foreach(transactionError error in errorList)
            {
                if (error.message != null)
                {
                    fErrorList.Add(error);
                }
            }

            return fErrorList;
        }

        public static transactionError validateCountry(string country)
        {
            string json;
            Boolean flag = false;

            using (StreamReader sr = new StreamReader(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"/Misc/countryCodes.json"))
            {
                json = sr.ReadToEnd();
            }

            var parsed = JArray.Parse(json);
            foreach(JToken code in parsed)
            {
                if(country == code.Last.First.Value<string>())
                {
                    flag = true;
                }
            }
            transactionError error = new transactionError();
            
            if(!flag)
            {
                error.section = "Country: ";
                error.message = country + "Is not a valid country code.";
            }

            return error;
        }

        public static transactionError validateCurrency(string currency)
        {
            string json;
            Boolean flag = false;

            using (StreamReader sr = new StreamReader(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"/Misc/currencyCodes.json"))
            {
                json = sr.ReadToEnd();
            }

            json = json.Replace('{', ' ');
            json = json.Replace('}', ' ');
            json = json.Replace('"', ' ');
            json = json.Replace(" ", "");
            string[] split = json.Split(',', ':');

            foreach (string x in split)
            {
                if (x == currency)
                {
                    flag = true;
                }
            }
            transactionError error = new transactionError();
        

            if (!flag)
            {
                error.section = "Currency: ";
                error.message = currency + "Is not a valid currency code.";
            }

            return error;
        }

    }
}