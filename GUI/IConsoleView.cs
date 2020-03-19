
namespace FreakyFashionTerminal.GUI
{
    interface IConsoleView
    {
        public IConsoleView EscapeView { get; }
        public void WriteInConsole();
    }
}
