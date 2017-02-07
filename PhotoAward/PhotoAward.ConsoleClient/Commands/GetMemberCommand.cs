using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PhotoAward.ConsoleClient.Commands
{
    public class GetMemberCommand : Command
    {
        public override async Task ExecuteAsync(string[] args)
        {
            try
            {
                var email = args[0];
                var client = new HttpClient();
                var result = await client.GetAsync(BaseUrl + "/Member/Get/" + email);
                await WriteResultAsync(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}