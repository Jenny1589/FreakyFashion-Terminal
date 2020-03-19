using FreakyFashionTerminal.GUI.Menues;
using FreakyFashionTerminal.Models;
using System;

namespace FreakyFashionTerminal.GUI.Forms
{
    class AddProductForm : PostForm<Product>
    {
        public AddProductForm()
            : base(requestUri: "product",
                   escapeView: new ProductMenu(),
                   fieldSet: new string[]
                            {
                                "Article number",
                                "Name",
                                "Description",
                                "Price",
                                "Image Url"
                            },
                   success: "Product was succesfully added",
                   fail: "Failed to add product")
        {}

        protected override Product ReadInput()
        {                                   
                var articleNumber = Console.ReadLine();
                NextInputLine();

                var name = Console.ReadLine();
                NextInputLine();

                var description = Console.ReadLine();
                NextInputLine();

                var price = double.Parse(Console.ReadLine());
                NextInputLine();

                var imageUri = Console.ReadLine();

            if (ConfirmInput("Is this correct? (Y)es or (N)o"))
            {
                return new Product()
                {
                    ArticleNumber = articleNumber,
                    Name = name,
                    Description = description,
                    Price = price,
                    ImageUri = new Uri(imageUri, UriKind.Relative)
                };
            }

            return null;            
        }
    }
}
