using FreakyFashionTerminal.GUI.Menues;
using FreakyFashionTerminal.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FreakyFashionTerminal.GUI.Forms
{
    class SignInForm : PostForm<User>
    {
        public SignInForm(IConsoleView escapeView)
            : base(
                  requestUri: "token",
                  escapeView: escapeView, 
                  fieldSet: new string[]
                    {
                        "Username",
                        "Password"
                    },
                  success: "You have signed in!",
                  fail: "Failed to sign in. Please try again")
        {}

        protected override bool ExecuteForm()
        {
            var isExecuted = base.ExecuteForm();

            if (!IsSuccess)
            {
                EscapeView = this;
            }
            else
            {
                var stringContent = Response.Content.ReadAsStringAsync().Result;
                var token = JsonConvert.DeserializeObject<Token>(stringContent);

                HttpCommunicator.SetAuthorizationHeader(token);

                Program.IsAnonymousUser = false;
            }            

            return isExecuted;
        }

        protected override User ReadInput()
        {
            var username = Console.ReadLine();
            
            NextInputLine();

            var password = ReadPasswordInput();

            return new User
            {
                Username = username,
                Password = password
            };
        }

        private string ReadPasswordInput()
        {
            var passwordBuilder = new StringBuilder();
            ConsoleKeyInfo keyPressed = new ConsoleKeyInfo();

            while (keyPressed.Key != ConsoleKey.Enter)
            {
                keyPressed = Console.ReadKey(true);

                if (keyPressed.Key != ConsoleKey.Enter && keyPressed.Key != ConsoleKey.Backspace)
                {
                    passwordBuilder.Append(keyPressed.KeyChar);
                    Console.Write("*");
                }
                else if (keyPressed.Key == ConsoleKey.Backspace)
                {
                    passwordBuilder.Remove(passwordBuilder.Length - 1, 1);
                    Console.Write("\b \b");
                }
            }

            return passwordBuilder.ToString();
        }
    }
}
