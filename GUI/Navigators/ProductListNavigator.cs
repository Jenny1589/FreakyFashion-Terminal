using FreakyFashionTerminal.GUI.Forms;
using FreakyFashionTerminal.GUI.Lists;
using System;

namespace FreakyFashionTerminal.GUI.Navigators
{
    class ProductListNavigator : INavigator
    {
        public IConsoleView View { get; }

        public ProductListNavigator(ProductList view)
        {
            View = view;
        }

        public IConsoleView GoTo(ConsoleKey keyPressed)
        {
            switch (keyPressed)
            {
                case ConsoleKey.V:
                    var view = new ProductDetailsForm();
                    view.InitializeForm(view.ProductList, view.EscapeAction);
                    
                    return view;

                case ConsoleKey.D:
                    view = new ProductDetailsForm(); 
                    view.InitializeForm(view.ProductList, view.DeleteAction);

                    if (Program.IsAnonymousUser) return new SignInForm(view);
                    return view;

                case ConsoleKey.E:
                    view = new ProductDetailsForm();
                    view.InitializeForm(view.ProductList, view.EditAction);

                    if (Program.IsAnonymousUser) return new SignInForm(view);

                    return view;

                case ConsoleKey.Escape:
                    return View.EscapeView;

                default:
                    return View;
            }
        }
    }
}
