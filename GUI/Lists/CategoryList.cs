using FreakyFashionTerminal.GUI.Menues;
using FreakyFashionTerminal.GUI.Navigators;
using FreakyFashionTerminal.Models;
using System;

namespace FreakyFashionTerminal.GUI.Lists
{
    class CategoryList : List<Category>
    {
        public CategoryList()
            : base("category", new CategoryMenu())
        {
            Navigator = new CategoryListNavigator(this);            
        }

        protected override void WriteListHeaders()
        {
            Console.WriteLine("Id".PadRight(10) + "Name");
        }

        protected override void WriteListMenu()
        {
            Console.WriteLine("(V)iew - (E)dit - (D)elete \t ESC: Category menu");
        }
    }
}
