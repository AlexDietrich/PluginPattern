using System.Collections.Generic;

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
