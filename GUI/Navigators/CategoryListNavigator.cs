using FreakyFashionTerminal.GUI.Forms;
using FreakyFashionTerminal.GUI.Lists;
using FreakyFashionTerminal.GUI.Menues;
using System;

namespace FreakyFashionTerminal.GUI.Navigators
{
    class CategoryListNavigator : INavigator
    {
        public IConsoleView View { get; }

        public CategoryListNavigator(IConsoleView view)
        {
            View = view;
        }

        public IConsoleView GoTo(ConsoleKey keyPressed)
        {
            switch (keyPressed)
            {
                case ConsoleKey.V:
                    var view = new CategoryDetailsForm();
                    view.InitializeForm(view.CategoryList, view.EscapeAction);
                    return view;

                case ConsoleKey.D:
                    view = new CategoryDetailsForm();
                    view.InitializeForm(view.CategoryList, view.DeleteAction);

                    if (Program.IsAnonymousUser) return new SignInForm(view);
                    
                    return view;

                case ConsoleKey.E:
                    view = new CategoryDetailsForm();
                    view.InitializeForm(view.CategoryList, view.EditAction);

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
