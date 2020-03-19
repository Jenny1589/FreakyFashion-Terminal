using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System;
using System.Threading;

namespace FreakyFashionTerminal.GUI.Forms
{
    abstract class PostForm<T> : Form<T>
    {
        protected string successMessage, failMessage;
        protected bool IsSuccess { get; private set; }
        protected HttpResponseMessage Response { get; private set; }

        public PostForm(string requestUri, IConsoleView escapeView, string[] fieldSet, string success, string fail) 
            : base(requestUri, escapeView, fieldSet)
            {
                successMessage = success;
                failMessage = fail;
            }

        protected override abstract T ReadInput();

        protected override bool ExecuteForm()
        {
            Console.CursorVisible = true;
            var item = ReadInput();

            if (item != null)
            {
                var serializedItem = JsonConvert.SerializeObject(item);
                var data = new StringContent(serializedItem, Encoding.UTF8, "application/json");

                Response = httpClient.PostAsync(requestUri, data).Result;

                Console.Clear();
                Console.CursorVisible = false;

                if (Response.IsSuccessStatusCode)
                {
                    Console.WriteLine(successMessage);
                    IsSuccess = true;
                }
                else
                {
                    Console.WriteLine(failMessage);
                    IsSuccess = false;
                }

                Thread.Sleep(2000);
                return true;
            }

            return false;
        }
    }
}
