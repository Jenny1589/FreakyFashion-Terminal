using FreakyFashionTerminal.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace FreakyFashionTerminal.GUI.Lists
{
    abstract class List<T> : IConsoleView, INavigable where T : IConsoleListable
    {
        protected IEnumerable<T> listItems;
        protected HttpClient httpClient = HttpCommunicator.Instance;

        public INavigator Navigator { get; protected set; }

        public IConsoleView EscapeView { get; }

        public List(string requestUri, IConsoleView escapeView)
        {
            EscapeView = escapeView;

            var response = httpClient.GetAsync(requestUri).Result;

            if (response.IsSuccessStatusCode)
            {
                var stringContent = response.Content.ReadAsStringAsync().Result;
                listItems = JsonConvert.DeserializeObject<IEnumerable<T>>(stringContent);
            }
        }

        public void WriteInConsole()
        {
            WriteListHeaders();
            Console.WriteLine(new string('-', Console.WindowWidth));

            foreach(var item in listItems)
            {
                Console.WriteLine(item.ToListItem());
            }

            Console.WriteLine();
            WriteListMenu();
        }

        protected abstract void WriteListHeaders();
        protected abstract void WriteListMenu();

    }
}
