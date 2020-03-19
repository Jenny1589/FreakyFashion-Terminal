using System;

namespace FreakyFashionTerminal.GUI.Menues
{
    abstract class Menu : IConsoleView, INavigable
    {
        protected string[] menuItems;
        protected string exitItem;

        public INavigator Navigator { get; protected set; }

        public IConsoleView EscapeView { get; }

        public Menu(IConsoleView escapeView)
        {
            EscapeView = escapeView;
        }

        public void WriteInConsole()
        {
            foreach(var item in menuItems)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();
            Console.WriteLine(exitItem);

            Console.CursorVisible = false;
        }
    }
}
