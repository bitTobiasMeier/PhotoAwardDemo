using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PhotoAward.PhotoManagement.Interfaces;

namespace PhotoAward.ConsoleClient.Commands
{
    public class GetInfosAboutAllPhotos : Command
    {
        public override async Task ExecuteAsync(string[] args)
        {
            try
            {
                var client = new HttpClient();
                var result = await client.GetAsync(BaseUrl + "/Photo/GetImagesOfMember/" );
                var json = await result.Content.ReadAsStringAsync();
                try
                {
                    var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PhotoMemberInfo>>(json);
                    foreach (var row in list.OrderBy(r => r.Email).ThenBy(r => r.PhotoId))
                    {
                        Console.WriteLine("{0}: {1}: {2} : {3}", row.Email, row.Title, row.FileName, row.PhotoId);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine(json);
                    throw;
                }
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
            return "Zeigt Informationen über alle Photos an";
        }
    }
}