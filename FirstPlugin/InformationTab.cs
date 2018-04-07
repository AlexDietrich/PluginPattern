using System;
using System.Globalization;
using System.IO;
using System.Xml;
using Interface;

namespace FirstPlugin
{
    internal class InformationTab : IProduct
    {
        private readonly BusinessLayer _businessLayer = new BusinessLayer();

        public string CustomerName { get; private set; } = "";
        public DateTime? ArriveDate { get; private set; } = null;
        public DateTime? LeaveDate { get; private set; } = null;
        public float InvoiceAmount { get; private set; } = 0;

        public InformationTab(string path)
        {
            try
            {
                ReadXml(path);
                WriteInfoInConsole();
                //simulate some input of customer-information by userinput
                Console.Write("Please set the arrive Date: ");
                    DateTime.TryParse(Console.ReadLine(), out var arrive);
                    ArriveDate = arrive;
                Console.Write("Please set the leave Date: ");
                    DateTime.TryParse(Console.ReadLine(), out var leave);
                    LeaveDate = leave;
                Console.Write("Please set the invoice amount: ");
                    float.TryParse(Console.ReadLine(), out var invoiceAmount);
                    InvoiceAmount = invoiceAmount;
                //Print the changes in the Console
                WriteInfoInConsole();
                UpdateCustomerFile(path);

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception was thrown: " + e.Message);
            }
        }

        #region XML Parsing
        /// <summary>
        /// Update the costumer file with the values from the input 
        /// </summary>
        /// <param name="path"></param>
        public void UpdateCustomerFile(string path)
        {
            try
            {
                var doc = new XmlDocument();
                doc.Load(path);
                // ReSharper disable once PossibleNullReferenceException
                doc.SelectSingleNode("customer/name").InnerText = CustomerName;
                // ReSharper disable once PossibleNullReferenceException
                doc.SelectSingleNode("customer/arrive").InnerText = ArriveDate.ToString();
                // ReSharper disable once PossibleNullReferenceException
                doc.SelectSingleNode("customer/leave").InnerText = LeaveDate.ToString();
                // ReSharper disable once PossibleNullReferenceException
                doc.SelectSingleNode("customer/money").InnerText = InvoiceAmount.ToString(CultureInfo.InvariantCulture);
                doc.Save(path);
            }
            catch (Exception e)
            {
                Console.WriteLine("An Exception was thrown: " + e.Message);
            }
        }



        /// <summary>
        /// Read the File which is given from the host to get the Data from the Person it's needed.
        /// </summary>
        /// <param name="path">Path to the XML-File of the customer</param>
        private void ReadXml(string path)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);

            //get the elements of the xml
            var customer = xmlDocument.GetElementsByTagName("name");
            var arrive = xmlDocument.GetElementsByTagName("arrive");
            var leave = xmlDocument.GetElementsByTagName("leave");
            var amountOfMoney = xmlDocument.GetElementsByTagName("money");

            //set all the local variables to the correct form out of the xml document
            if(customer[0].InnerText != string.Empty)    SetCostumerName(customer[0].InnerText);
            if(arrive[0].InnerText != string.Empty)    SetArriveDate(DateTime.Parse(arrive[0].InnerText));
            if(leave[0].InnerText != string.Empty)    SetLeaveDate(DateTime.Parse(leave[0].InnerText));
            if(amountOfMoney[0].InnerText != string.Empty)    SetInvoiceAmount(float.Parse(amountOfMoney[0].InnerText, CultureInfo.InvariantCulture));
        }
        #endregion

        #region Setter
        public void SetInvoiceAmount(float money)
        {
            this.InvoiceAmount = money;
        }

        public void SetCostumerName(string name)
        {
            if (_businessLayer.CostumerNameIsValid(name))
            {
                CustomerName = name;
            }
            else
            {
                throw new InvalidDataException("Name is not valid");
            }
        }

        public void SetArriveDate(DateTime? arriveDate)
        {
            if (_businessLayer.ArriveDateIsValid(arriveDate, LeaveDate))
            {
                ArriveDate = arriveDate;
            }
            else
            {
                throw new InvalidDataException("Arrive Date is not valid");
            }
        }

        public void SetLeaveDate(DateTime? leaveDate)
        {
            if (_businessLayer.LeaveDateIsValid(ArriveDate, leaveDate))
            {
                LeaveDate = leaveDate;
            }
            else
            {
                throw new InvalidDataException("Leave Date is not valid");
            }
        }
        #endregion

        #region InfoFunctions
        public void PrintProductName()
        {
            Console.WriteLine("Information - Tab");
        }

        public void WriteInfoInConsole()
        {
            Console.WriteLine("Costumer: " + CustomerName);
            Console.WriteLine("Arrive: " + ArriveDate?.Date);
            Console.WriteLine("Leave: " + LeaveDate?.Date);
            Console.WriteLine("Invoice Amount: " + InvoiceAmount);
        }
        #endregion
    }
}
