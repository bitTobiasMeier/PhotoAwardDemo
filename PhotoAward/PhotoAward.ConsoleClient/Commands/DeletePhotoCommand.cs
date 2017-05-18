using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Converters;

namespace PhotoAward.ConsoleClient.Commands
{
    public class DeletePhotoCommand : Command
    {
        public override async Task ExecuteAsync(string[] args)
        {
            try
            {
                var photoId = args[0];
                var client = CreateClientWithAuthorizationHeader();
                var result = await client.PostAsJsonAsync(BaseUrl + "/Photo/Delete", photoId);
                var datetimeconverter = new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-ddHH:mm:ss" };
                Console.WriteLine(await result.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public override string GetArguments()
        {
            var sb = new StringBuilder();
            sb.Append("email pathToFile title");
            return sb.ToString();
        }

        public override string GetDescription()
        {
            return "Löscht das Photo mit der angegebenen Id";
        }
    }
}