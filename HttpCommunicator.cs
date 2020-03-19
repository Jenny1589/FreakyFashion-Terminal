using FreakyFashionTerminal.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace FreakyFashionTerminal
{
    class HttpCommunicator
    {
        private static HttpClient httpClient;

        public static HttpClient Instance 
        { 
            get
            {
                if (httpClient == null)
                {
                    httpClient = new HttpClient();

                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "HighscoreTerminal");
                    httpClient.BaseAddress = new Uri("https://localhost:5001/api/");
                }

                return httpClient;
            }
        }

        public static bool SetAuthorizationHeader(Token token)
        {
            if(httpClient != null)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.Value);
                return true;
            }

            return false;
        }
    }
}
