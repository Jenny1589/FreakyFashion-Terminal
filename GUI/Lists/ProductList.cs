using FreakyFashionTerminal.GUI.Menues;
using FreakyFashionTerminal.GUI.Navigators;
using FreakyFashionTerminal.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace FreakyFashionTerminal.GUI.Lists
{
    class ProductList : List<Product>
    {
        public ProductList()
            : base("product", new ProductMenu())
        {
            Navigator = new ProductListNavigator(this);            
        }

        protected override void WriteListHeaders()
        {
            Console.WriteLine("Id".PadRight(10) + "Name");
        }

        protected override void WriteListMenu()
        {
            Console.WriteLine("(V)iew - (E)dit - (D)elete \t ESC: Product menu");
        }
    }
}
