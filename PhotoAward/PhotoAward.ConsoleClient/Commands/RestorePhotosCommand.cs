using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAward.ConsoleClient.Commands
{
    public class RestorePhotosCommand : Command
    {
        public override async Task ExecuteAsync(string[] args)
        {
            try
            {
                var client = new HttpClient();
                var result = await client.GetAsync(BaseUrl + "/Photo/Restore/");
                var json = await result.Content.ReadAsStringAsync();
                Console.WriteLine(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public override string GetArguments()
        {
            var sb = new StringBuilder();
            sb.Append("");
            return sb.ToString();
        }

        public override string GetDescription()
        {
            return "Restore-Test";
        }
    }
}