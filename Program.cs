using FreakyFashionTerminal.GUI;
using FreakyFashionTerminal.GUI.Forms;
using FreakyFashionTerminal.GUI.Menues;
using FreakyFashionTerminal.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;

namespace FreakyFashionTerminal
{
    class Program
    {
        public static bool IsAnonymousUser { get; set; } = true;
        private static readonly HttpClient httpClient = new HttpClient();

        static void Main(string[] args)
        {
            HttpCommunicator.SetAuthorizationHeader(AnonymousUserToken());

            IConsoleView view = new MainMenu();
            
            while (view != null)
            {
                view.WriteInConsole();
                
                if(view is INavigable menu)
                {
                    var keyPressed = Console.ReadKey(true).Key;
                    view = menu.Navigator.GoTo(keyPressed);
                }
                else
                {
                    view = view.EscapeView;
                }

                Console.Clear();
            }            
        } 
        
        private static Token AnonymousUserToken()
        {
            var user = new User
            {
                Username = "test@nomail.com",
                Password = "Secret123!"
            };

            var serializedUser = JsonConvert.SerializeObject(user);
            var data = new StringContent(serializedUser, Encoding.UTF8, "application/json");

            var response = HttpCommunicator.Instance.PostAsync("token", data).Result;

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Failed to initialize system. Try again later");
                Thread.Sleep(2000);
                Environment.Exit(0);
                return null;
            }

            var stringToken = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<Token>(stringToken);
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
            }
            else
            {
                Console.WriteLine(response.StatusCode.ToString());
            }
        }
    }
}
