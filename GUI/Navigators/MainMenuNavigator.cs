using FreakyFashionTerminal.GUI.Menues;
using System;

namespace FreakyFashionTerminal.GUI.Navigators
{
    class MainMenuNavigator : INavigator
    {       

        public IConsoleView View { get; }

        public MainMenuNavigator(IConsoleView view)
        {
            View = view;
        }

        public IConsoleView GoTo(ConsoleKey keyPressed)
        {
            switch (keyPressed)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    return new ProductMenu();

                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    return new CategoryMenu();

                case ConsoleKey.Escape:
                    return View.EscapeView;

                default:
                    return View;
            }
        }
    }
}
