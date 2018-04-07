using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IPlugin
    {
        string GetName();

        void SetParameter(string name);

        void AddProduct(IProduct product);

        IEnumerable<IProduct> GetAllProducts();
    }
}
