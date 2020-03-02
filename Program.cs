using FreakyFashionTerminal.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;

namespace FreakyFashionTerminal
{
    class Program
    {
        static readonly HttpClient httpClient = new HttpClient();

        static void Main(string[] args)
        {

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            httpClient.DefaultRequestHeaders.Add("User-Agent", "HighscoreTerminal");

            httpClient.BaseAddress = new Uri("https://localhost:5001/api/");

            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();

                Console.WriteLine("1. Products");
                Console.WriteLine("2. Categories");
                Console.WriteLine();
                Console.WriteLine("ESC: Exit ");

                Console.CursorVisible = false;

                var keyPressed = Console.ReadKey(true);

                Console.Clear();

                switch (keyPressed.Key)
                {

                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        ProductMenu();
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        CategoryMenu();
                        break;

                    case ConsoleKey.Escape:
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("\nInvalid menu choice. Please try again!");
                        Thread.Sleep(2000);
                        break;
                }
            }
        }

        private static void CategoryMenu()
        {
            bool showCategoryMenu = true;

            while (showCategoryMenu)
            {
                Console.Clear();

                Console.WriteLine("1. List categories");
                Console.WriteLine();
                Console.WriteLine("ESC: Main menu");

                Console.CursorVisible = false;

                var keyPressed = Console.ReadKey(true);

                Console.Clear();

                switch (keyPressed.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        ListCategories();
                        break;

                    case ConsoleKey.Escape:
                        showCategoryMenu = false;
                        break;

                    default:
                        Console.WriteLine("\nInvalid menu choice. Please try again!");
                        Thread.Sleep(2000);
                        break;
                }
            }
        }

        private static void ProductMenu()
        {
            bool showProductMenu = true;

            while (showProductMenu)
            {
                Console.Clear();

                Console.WriteLine("1. List products");
                Console.WriteLine();
                Console.WriteLine("ESC: Main menu");

                Console.CursorVisible = false;

                var keyPressed = Console.ReadKey(true);

                Console.Clear();

                switch (keyPressed.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        ListProducts();
                        break;

                    case ConsoleKey.Escape:
                        showProductMenu = false;
                        break;

                    default:
                        Console.WriteLine("\nInvalid menu choice. Please try again!");
                        Thread.Sleep(2000);
                        break;
                }
            }
        }

        private static void ListCategories()
        {
            var response = httpClient.GetAsync("category").Result;

            if (response.IsSuccessStatusCode)
            {
                var stringContent = response.Content.ReadAsStringAsync().Result;
                var categories = JsonConvert.DeserializeObject<IEnumerable<Category>>(stringContent);

                WriteCategoryList(categories);
            }
        }

        private static void ListProducts()
        {
            var response = httpClient.GetAsync("product").Result;

            if (response.IsSuccessStatusCode)
            {
                var stringContent = response.Content.ReadAsStringAsync().Result;
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(stringContent);

                WriteProductList(products);
            }
        }

        private static void WriteCategoryList(IEnumerable<Category> categories)
        {
            bool showCategoryList = true;

            while (showCategoryList)
            {
                Console.Clear();

                Console.Write("ID".PadRight(10));
                Console.WriteLine("NAME");

                Console.WriteLine(new string('-', Console.WindowWidth));

                foreach (var category in categories)
                {
                    Console.Write(category.Id.ToString().PadRight(10));
                    Console.WriteLine(category.Name);
                }

                Console.WriteLine();
                Console.WriteLine("(V)iew \t ESC: Menu");

                var keyPressed = Console.ReadKey(true);

                Console.Clear();

                switch (keyPressed.Key)
                {
                    case ConsoleKey.V:
                        Console.WriteLine("View category with ID: ");
                        string categoryId = Console.ReadLine();

                        ShowCategoryDetails(int.Parse(categoryId));
                        break;
                    case ConsoleKey.Escape:
                        showCategoryList = false;
                        break;
                    default:
                        break;
                }
            }
        }

        private static void WriteProductList(IEnumerable<Product> products)
        {
            bool showProductList = true;
            
            while (showProductList)
            {
                Console.Clear(); 

                Console.Write("ID".PadRight(10));
                Console.WriteLine("NAME");

                Console.WriteLine(new string('-', Console.WindowWidth));

                foreach (var product in products)
                {
                    Console.Write(product.Id.ToString().PadRight(10));
                    Console.WriteLine(product.Name);
                }

                Console.WriteLine();
                Console.WriteLine("(V)iew \t ESC: Menu");

                var keyPressed = Console.ReadKey(true);

                Console.Clear();

                switch (keyPressed.Key)
                {
                    case ConsoleKey.V:
                        Console.WriteLine("View product with ID: ");
                        string productId = Console.ReadLine();

                        ShowProductDetails(int.Parse(productId));
                        break;
                    case ConsoleKey.Escape:
                        showProductList = false;
                        break;
                    default:
                        break;
                }
            }
        }

        private static void ShowCategoryDetails(int categoryId)
        {
            Console.Clear();
            var response = httpClient.GetAsync($"category/{categoryId}").Result;

            if (response.IsSuccessStatusCode)
            {
                var stringContent = response.Content.ReadAsStringAsync().Result;
                var category = JsonConvert.DeserializeObject<Category>(stringContent);

                Console.WriteLine($"ID: {category.Id}");
                Console.WriteLine($"NAME: {category.Name}");
                Console.WriteLine($"IMAGE URL: {category.ImageUri.ToString()}");
                Console.WriteLine();
                Console.WriteLine("ESC: Category list");

                var keyPressed = Console.ReadKey(true);

                while (keyPressed.Key != ConsoleKey.Escape)
                {
                    keyPressed = Console.ReadKey(true);
                }
            }
            else
            {
                Console.WriteLine(response.StatusCode.ToString());
            }
        }

        private static void ShowProductDetails(int productId)
        {
            Console.Clear();
            var response = httpClient.GetAsync($"product/{productId}").Result;

            if (response.IsSuccessStatusCode)
            {
                var stringContent = response.Content.ReadAsStringAsync().Result;
                var product = JsonConvert.DeserializeObject<Product>(stringContent);

                Console.WriteLine($"ID: {product.Id}");
                Console.WriteLine($"NAME: {product.Name}");
                Console.WriteLine($"DESCRIPTION: {product.Description}");
                Console.WriteLine($"PRICE: {string.Format("{0:c}", product.Price)}");
                Console.Write("CATEGORIES: ");

                foreach(var productCategory in product.ProductCategories)
                {
                    Console.Write($"{productCategory.Category.Name}, ");
                }

                Console.WriteLine("\b\b ");
                Console.WriteLine();
                Console.WriteLine("ESC: Product list");

                var keyPressed = Console.ReadKey(true);

                while(keyPressed.Key != ConsoleKey.Escape)
                {
                    keyPressed = Console.ReadKey(true);
                }
            }
            else
            {
                Console.WriteLine(response.StatusCode.ToString());
            }
        }
    }
}
