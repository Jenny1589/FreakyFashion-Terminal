using FreakyFashionTerminal.GUI.Forms;
using FreakyFashionTerminal.GUI.Lists;
using FreakyFashionTerminal.GUI.Menues;
using System;

namespace FreakyFashionTerminal.GUI.Navigators
{
    class CategoryMenuNavigator : INavigator
    {
        public IConsoleView View { get; }

        public CategoryMenuNavigator(IConsoleView view)
        {
            View = view;
        }

        public IConsoleView GoTo(ConsoleKey keyPressed)
        {
            switch (keyPressed)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    return new CategoryList();

                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    if (Program.IsAnonymousUser) return new SignInForm(new AddCategoryForm());
                    return new AddCategoryForm();

                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    return new AddProductToCategoryForm();

                case ConsoleKey.Escape:
                    return View.EscapeView;

                default:
                    return View;
            }
        }
    }
}
