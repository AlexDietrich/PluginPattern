using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Interface;

namespace Plugin
{
    [SuppressMessage("ReSharper", "ArrangeTypeModifiers")]
    static class Program
    {
        [SuppressMessage("ReSharper", "ArrangeTypeMemberModifiers")]
        static void Main(string[] args)
        {
            var businessLayer = new BusinessLayer();
            //Each line is 1 Assembly
            var file = File.ReadAllLines("path.config");
            //Add all Plugins from the config file into the List of Plugins 
            List<IPlugin> plugins = file.Select(LoadAssembly).Where(plugin => plugin != null).ToList();
            //If no plugins are loaded, return the main function because it's useless to continue ;) 
            if (plugins.Count <= 0) return;
            //Get the first Plugin out of the list to print all the products
            var firstPlugin = plugins[0];

            //Switch to Plugin with the data
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Switch to Plugin: ");


            //set the parameter for the costumer you want. In my version there is just one customer - normally you have the file-path in a Database.
            firstPlugin.SetParameter("customer.xml");


            //Get info in Hostapplication
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("In the Hostapplication: ");
            var customer = businessLayer.GetDataFromCustomer("customer.xml");
            foreach (var information in customer)
            {
                Console.WriteLine(information.Key + ": " + information.Value);
            }
        }

        private static IPlugin LoadAssembly(string newPlugin)
        {
            try
            {
                var type = Type.GetType(newPlugin);
                var plugin = (IPlugin)Activator.CreateInstance(type);
                var name = plugin?.GetName();
                Console.WriteLine("Plugin with the name: " + name + " is loaded!");
                return plugin;
            }
            catch (Exception ex) when (
                ex is FileLoadException ||
                ex is BadImageFormatException)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
