using FreakyFashionTerminal.GUI.Menues;
using FreakyFashionTerminal.Models;
using System;

namespace FreakyFashionTerminal.GUI.Forms
{
    class AddProductToCategoryForm : PostForm<ProductCategory>
    {
        public AddProductToCategoryForm()
            : base(requestUri: "productcategory",
                   escapeView: new CategoryMenu(),
                   fieldSet: new string[]
                            {
                                "Product id",
                                "Category id"
                            },
                   success: "Product was succesfully added to category",
                   fail: "Failed to add product to category")
        {}

        protected override ProductCategory ReadInput()
        {            
                var productId = int.Parse(Console.ReadLine());
                NextInputLine();

                var categoryId = int.Parse(Console.ReadLine());

            if (ConfirmInput("Is this correct? (Y)es or (N)o"))
            {
                return new ProductCategory()
                {
                    ProductId = productId,
                    CategoryId = categoryId
                };
            }

            return null;            
        }
    }
}
