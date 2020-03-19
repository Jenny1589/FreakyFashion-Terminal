using FreakyFashionTerminal.Models;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace FreakyFashionTerminal.GUI.Forms
{
    class ProductDetailsForm : RequestIdForm<Product>
    {
        public ProductDetailsForm()
            : base("product")
        {}
        
        protected override void WriteDetails(Product entity)
        {
            Console.WriteLine($"ID: {entity.Id}");
            Console.WriteLine($"ARTICLE NUMBER: {entity.ArticleNumber}");
            Console.WriteLine($"NAME: {entity.Name}");
            Console.WriteLine($"DESCRIPTION: {entity.Description}");
            Console.WriteLine($"PRICE: {string.Format("{0:c}", entity.Price)}");
            Console.WriteLine($"IMAGE URI: {entity.ImageUri}");
            Console.Write("CATEGORIES: ");

            foreach (var productCategory in entity.ProductCategories)
            {
                Console.Write($"{productCategory.Category.Name}, ");
            }

            Console.WriteLine("\b\b ");
        }

        public override bool EditAction(Func<IConsoleView> escapeView)
        {
            var shallEdit = true;
            var patchDoc = new JsonPatchDocument<Product>();

            while (shallEdit)
            {
                Console.WriteLine(new string('=', Console.WindowWidth));
                Console.WriteLine();
                Console.WriteLine("Choose data to update:");
                Console.WriteLine();
                Console.WriteLine("1. ARTICLE NUMBER");
                Console.WriteLine("2. NAME");
                Console.WriteLine("3. DESCRIPTION");
                Console.WriteLine("4. PRICE");
                Console.WriteLine("5. IMAGE URI");
                Console.WriteLine();

                var isValidKey = false;

                do
                {
                    var keyPressed = Console.ReadKey(true).Key;

                    switch (keyPressed)
                    {
                        case ConsoleKey.NumPad1:
                        case ConsoleKey.D1:
                            isValidKey = true;

                            Console.Write("ARTICLE NUMBER: ");
                            var value = Console.ReadLine();

                            patchDoc.Replace(p => p.ArticleNumber, value);
                            Console.WriteLine();
                            shallEdit = ConfirmInput("Continue to update? (Y)es or (N)o");
                            break;

                        case ConsoleKey.NumPad2:
                        case ConsoleKey.D2:
                            isValidKey = true;

                            Console.Write("NAME: ");
                            value = Console.ReadLine();

                            patchDoc.Replace(p => p.Name, value);
                            Console.WriteLine();
                            shallEdit = ConfirmInput("Continue to update? (Y)es or (N)o");
                            break;

                        case ConsoleKey.NumPad3:
                        case ConsoleKey.D3:
                            isValidKey = true;

                            Console.Write("DESCRIPTION: ");
                            value = Console.ReadLine();

                            patchDoc.Replace(p => p.Description, value);
                            Console.WriteLine();
                            shallEdit = ConfirmInput("Continue to update? (Y)es or (N)o");
                            break;

                        case ConsoleKey.NumPad4:
                        case ConsoleKey.D4:
                            isValidKey = true;

                            Console.Write("Price: ");
                            value = Console.ReadLine();

                            patchDoc.Replace(p => p.Price, double.Parse(value));
                            Console.WriteLine();
                            shallEdit = ConfirmInput("Continue to update? (Y)es or (N)o");
                            break;

                        case ConsoleKey.NumPad5:
                        case ConsoleKey.D5:
                            isValidKey = true;

                            Console.Write("IMAGE URI: ");
                            value = Console.ReadLine();

                            patchDoc.Replace(c => c.ImageUri, new Uri(value, UriKind.Relative));

                            Console.WriteLine();
                            shallEdit = ConfirmInput("Continue to update? (Y)es or (N)o");
                            break;

                        default:
                            isValidKey = false;
                            break;
                    }

                } while (!isValidKey);
            }

            var serializedItem = JsonConvert.SerializeObject(patchDoc);
            var data = new StringContent(serializedItem, Encoding.UTF8, "application/json");

            var response = httpClient.PatchAsync(requestUri, data).Result;

            EscapeView = escapeView();

            return response.IsSuccessStatusCode ? true : false;
        }
    }
}
