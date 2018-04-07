using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Plugin
{
    internal class BusinessLayer
    {
        internal IDictionary<string, string> GetDataFromCustomer(string path)
        {
            IDictionary<string, string> data = new Dictionary<string, string>();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);

            //get the elements of the xml
            var customer = xmlDocument.GetElementsByTagName("name");
            var arrive = xmlDocument.GetElementsByTagName("arrive");
            var leave = xmlDocument.GetElementsByTagName("leave");
            var amountOfMoney = xmlDocument.GetElementsByTagName("money");

            //set all the local variables to the correct form out of the xml document
            if (customer[0].InnerText != string.Empty) data.Add("Name", customer[0].InnerText);
            if (arrive[0].InnerText != string.Empty) data.Add("Arrive", arrive[0].InnerText);
            if (leave[0].InnerText != string.Empty) data.Add("Leave", leave[0].InnerText);
            if (amountOfMoney[0].InnerText != string.Empty) data.Add("Invoice amount", amountOfMoney[0].InnerText);
            return data; 
        }
    }
}
