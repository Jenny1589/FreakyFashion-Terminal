using FreakyFashionTerminal.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace FreakyFashionTerminal.GUI.Forms
{
    class CategoryDetailsForm : RequestIdForm<Category>
    {
        public CategoryDetailsForm()
            : base("category")
        {}

        public override bool EditAction(Func<IConsoleView> escapeView)
        {
            
            var shallEdit = true;
            var patchDoc = new JsonPatchDocument<Category>();

            while (shallEdit)
            {
                Console.WriteLine();
                Console.WriteLine("Choose data to update:");
                Console.WriteLine();
                Console.WriteLine("1. NAME");
                Console.WriteLine("2. IMAGE URI");
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

                            Console.Write("NAME: ");
                            var value = Console.ReadLine();

                            patchDoc.Replace(c => c.Name, value);
                            Console.WriteLine();
                            shallEdit = ConfirmInput("Continue to update? (Y)es or (N)o");
                            break;

                        case ConsoleKey.NumPad2:
                        case ConsoleKey.D2:
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

        protected override void WriteDetails(Category entity)
        {
            Console.WriteLine($"ID: {entity.Id}");
            Console.WriteLine($"NAME: {entity.Name}");
            Console.WriteLine($"IMAGE URI: {entity.ImageUri}");
        }
    }
}
