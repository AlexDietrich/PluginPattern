using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Interface;

namespace FirstPlugin
{
    public class FirstPlugin : IPlugin
    {
        private readonly List<IProduct> _products = new List<IProduct>();

        public void SetParameter(string path)
        {
            _products.Add(new InformationTab(path));
            _products.Add(new PdfPrinter(path));
        }

        public string GetName()
        {
            return "FirstPlugin";
        }

        public void AddProduct(IProduct product)
        {
            if(!_products.Contains(product)) _products.Add(product);
        }

        public IEnumerable<IProduct> GetAllProducts()
        {
            return _products;
        }
    }
}
