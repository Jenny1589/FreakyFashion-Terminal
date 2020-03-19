using FreakyFashionTerminal.GUI.Navigators;

namespace FreakyFashionTerminal.GUI.Menues
{
    class CategoryMenu : Menu
    {
        public CategoryMenu()
            : base(new MainMenu())
        {
            menuItems = new string[]
            {
                "1. List categories",
                "2. Add category",
                "3. Add product to category"
            };

            exitItem = "ESC: Back to main menu";

            Navigator = new CategoryMenuNavigator(this);
        }
    }
}
