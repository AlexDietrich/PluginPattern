using System;
using Interface;

namespace FirstPlugin
{
    class PdfPrinter : IProduct
    {
        private string _path;

        public PdfPrinter(string path)
        {
            this._path = path;
        }
        public void PrintProductName()
        {
            Console.WriteLine("I'm the PDF-Printer");
        }
    }
}
