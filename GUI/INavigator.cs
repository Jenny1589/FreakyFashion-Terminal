using System;

namespace FreakyFashionTerminal.GUI
{
    interface INavigator
    {
        public IConsoleView View { get; }
        public IConsoleView GoTo(ConsoleKey keyPressed);
    }
}
