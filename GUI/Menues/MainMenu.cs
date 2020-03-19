using FreakyFashionTerminal.GUI.Navigators;

namespace FreakyFashionTerminal.GUI.Menues
{
    class MainMenu : Menu
    {
        public MainMenu()
            : base(null)
        {
            menuItems = new string[]
            {
                "1. Products",
                "2. Categories"
            };

            exitItem = "ESC: Quit";

            Navigator = new MainMenuNavigator(this);
        }
    }
}
