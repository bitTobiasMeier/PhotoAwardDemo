using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using PhotoAward.MemberManagement.Interfaces;
using PhotoAward.PhotoManagement.Interfaces;

namespace PhotoAward.ConsoleClient.Commands
{
    public class GetPhotosOfMemberCommand : Command
    {
        public override async Task ExecuteAsync(string[] args)
        {
            try
            {
                var email = args[0];
                var targetDir = args[1];
                var client = new HttpClient();
                var result = await client.GetAsync(BaseUrl + "/Photo/GetThumbnailsOfMember/" + email);
                var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PhotoManagementData>>(await result.Content.ReadAsStringAsync());
                foreach (var row in list)
                {
                    var name = Path.Combine(targetDir, row.FileName);
                    File.WriteAllBytes(name,row.ThumbnailBytes);
                }
                await WriteResultAsync(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}