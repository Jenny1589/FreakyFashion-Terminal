using FreakyFashionTerminal.GUI.Lists;
using Newtonsoft.Json;
using System;
using System.Threading;

namespace FreakyFashionTerminal.GUI.Forms
{
    abstract class RequestIdForm<T> : Form<int>
    {
        private Func<Func<IConsoleView>, bool> formAction;
        private Func<IConsoleView> escapeViewManager;

        private T entity;

        public RequestIdForm(string requestUri)
            : base(requestUri, null, new string[] { "Id" })
        {}

        protected override bool ExecuteForm()
        {
            var id = ReadInput();
            requestUri += $"/{id}";

            GetEntity();

            Console.Clear();
            if (entity == null)
            {
                Console.WriteLine($"Failed to find entity with id {id}");
                Thread.Sleep(2000);
                return false;
            }
            else
            {
                WriteDetails(entity);
                Console.WriteLine();                

                return formAction(escapeViewManager);
            }            
        }

        private void GetEntity()
        {
            var response = httpClient.GetAsync(requestUri).Result;
            var stringContent = response.Content.ReadAsStringAsync().Result;

            entity = JsonConvert.DeserializeObject<T>(stringContent);
        }

        protected abstract void WriteDetails(T entity);

        public abstract bool EditAction(Func<IConsoleView> escapeView);

        public bool EscapeAction(Func<IConsoleView> escapeView)
        {
            EscapeView = escapeView();
            Console.WriteLine("ESC: Go back to list");

            ConsoleKey keyPressed;

            do
            {
                keyPressed = Console.ReadKey(true).Key;
            } while (keyPressed != ConsoleKey.Escape);

            return true;
        }

        public void InitializeForm(Func<IConsoleView> escapeViewManager, Func<Func<IConsoleView>, bool> formAction)
        {
            this.escapeViewManager = escapeViewManager;
            this.formAction = formAction;
        }

        public bool DeleteAction(Func<IConsoleView> escapeView)
        {
            bool isConfirmed = ConfirmInput("Do you want to delete? (Y)es or (N)o");
            bool isSuccess = false;

            Console.Clear();

            if (isConfirmed)
            {
                var response = httpClient.DeleteAsync(requestUri).Result;

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Deleted!");
                    isSuccess = true;
                }
                else
                {
                    Console.WriteLine("Could not delete");
                }

                Thread.Sleep(2000);
            }

            EscapeView = escapeViewManager();
            return isSuccess;
        }

        public IConsoleView ProductList()
        {
            return new ProductList();
        }

        public IConsoleView CategoryList()
        {
            return new CategoryList();
        }

        protected override int ReadInput()
        {
            return int.Parse(Console.ReadLine());
        }
    }
}
