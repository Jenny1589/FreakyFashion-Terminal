using FreakyFashionTerminal.GUI.Menues;
using FreakyFashionTerminal.Models;
using System;

namespace FreakyFashionTerminal.GUI.Forms
{
    class AddCategoryForm : PostForm<Category>
    {
        public AddCategoryForm()
            : base(requestUri: "category",
                   escapeView: new CategoryMenu(),
                   fieldSet: new string[]
                            {
                                "Name",
                                "Image Url"
                            },
                   success: "Category was succesfully added",
                   fail: "Failed to add category")
        {}

        protected override Category ReadInput()
        {            
                var name = Console.ReadLine();
                NextInputLine();

                var imageUri = Console.ReadLine();

            if (ConfirmInput("Is this correct? (Y)es or (N)o"))
            {
                return new Category()
                {
                    Name = name,
                    ImageUri = new Uri(imageUri, UriKind.Relative)
                };
            }

            return null;            
        }
    }
}
