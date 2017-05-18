using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace PhotoAward.ConsoleClient.Commands
{
    public class Command
    {
        public string BaseUrl { get; } = "http://localhost:8200/api";

        public Command()
        {
            var url = ConfigurationManager.AppSettings["url"];
            if (!string.IsNullOrEmpty(url))
            {
                this.BaseUrl = url;
            }
        }

#pragma warning disable 1998
        public virtual async Task ExecuteAsync(string[] args)
#pragma warning restore 1998
        {
            
        }

        protected  async Task WriteResultAsync(HttpResponseMessage result)
        {
            Console.WriteLine("StatusCode:" + result.StatusCode);
            Console.WriteLine(await result.Content.ReadAsStringAsync());
        }
        

        public virtual string GetArguments()
        {
            return "";
        }

        public virtual string GetDescription()
        {
            return "";
        }

        protected static HttpClient CreateClientWithAuthorizationHeader()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + System.IO.File.ReadAllText("Bearer.token"));
            return client;
        }
    }
}