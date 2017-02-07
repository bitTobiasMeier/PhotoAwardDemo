using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PhotoAward.ConsoleClient.Commands
{
    public class PingCommand : Command
    {
        public override async Task ExecuteAsync(string[] args)
        {
            try
            {
                var client = new HttpClient();
                var result = await client.GetAsync(BaseUrl + "/Test/Ping");
                await WriteResultAsync(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}