using FreakyFashionTerminal.GUI.Navigators;

namespace FreakyFashionTerminal.GUI.Menues
{
    class ProductMenu : Menu
    {
        public ProductMenu()
            : base (new MainMenu())
        {
            menuItems = new string[]
            {
                "1. List products",
                "2. Add product"
            };

            exitItem = "ESC: Back to main menu";

            Navigator = new ProductMenuNavigator(this);
        }
    }
}
