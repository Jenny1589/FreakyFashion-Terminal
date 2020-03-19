using System;
using System.Linq;
using System.Net.Http;

namespace FreakyFashionTerminal.GUI.Forms
{
    abstract class Form<T> : IConsoleView
    {
        private int cursorX = 0;
        private int cursorY = 0;

        protected HttpClient httpClient = HttpCommunicator.Instance;
        protected string requestUri;
        protected string[] fieldSet;

        public IConsoleView EscapeView { get; protected set; }

        public Form(string requestUri, IConsoleView escapeView, string[] fieldSet)
        {
            this.requestUri = requestUri;
            this.fieldSet = fieldSet;
            EscapeView = escapeView;
        }

        public void WriteInConsole()
        {
            bool isSuccess = false;

            do
            {
                Console.Clear();
                cursorX = 0;
                cursorY = 0;

                int fieldWidth = fieldSet.Select(s => s.Length)
                    .OrderByDescending(n => n)
                    .First();

                foreach (var field in fieldSet)
                {
                    Console.WriteLine(field.ToUpper().PadLeft(fieldWidth) + ": ");
                    NextInputLine();
                }

                cursorX = fieldWidth + 2;
                cursorY = 0;

                Console.SetCursorPosition(cursorX, cursorY);
                Console.CursorVisible = true;

                isSuccess = ExecuteForm();

            } while (!isSuccess);            
        }

        protected abstract bool ExecuteForm();
        protected abstract T ReadInput();

        protected bool ConfirmInput(string question)
        {
            Console.CursorVisible = false;
            //cursorX = 0;
            //NextInputLine();

            Console.WriteLine(question);
            
            ConsoleKeyInfo keyPressed;

            do
            {
                keyPressed = Console.ReadKey(true);
            } while (keyPressed.Key != ConsoleKey.N && keyPressed.Key != ConsoleKey.Y);

            return keyPressed.Key == ConsoleKey.Y;
        }

        protected void NextInputLine()
        {
            cursorY += 2;
            Console.SetCursorPosition(cursorX, cursorY);
        }
    }
}
