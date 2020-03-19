using FreakyFashionTerminal.GUI.Forms;
using FreakyFashionTerminal.GUI.Lists;
using FreakyFashionTerminal.GUI.Menues;
using System;

namespace FreakyFashionTerminal.GUI.Navigators
{
    class ProductMenuNavigator : INavigator
    {
        public IConsoleView View { get; }

        public ProductMenuNavigator(IConsoleView view)
        {
            View = view;
        }

        public IConsoleView GoTo(ConsoleKey keyPressed)
        {
            switch (keyPressed)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    return new ProductList();

                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    if (Program.IsAnonymousUser) return new SignInForm(new AddProductForm());
                    return new AddProductForm();

                case ConsoleKey.Escape:
                    return View.EscapeView;

                default:
                    return new ProductMenu();
            }
        }
    }
}
