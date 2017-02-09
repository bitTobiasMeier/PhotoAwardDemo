using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PhotoAward.ConsoleClient.Commands
{
    public class Command
    {
        public string BaseUrl { get; } = "http://localhost:8200/api";

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
    }
}