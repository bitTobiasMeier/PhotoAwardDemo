using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using PhotoAward.PhotoManagement.Interfaces;

namespace PhotoAward.ConsoleClient.Commands
{
    public class GetPhotoCommentsCommand : Command
    {
        public override async Task ExecuteAsync(string[] args)
        {
            try
            {
                var photoId = new Guid(args[0]);
                var email = args[1];
                var targetDir = args[2];
                var client = new HttpClient();
                var result = await client.GetAsync(BaseUrl + "/Photo/GetComments/" + photoId.ToString());
                var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CommentData>>(await result.Content.ReadAsStringAsync());
                foreach (var row in list)
                {
                    Console.WriteLine(row.CommentDate.ToString(CultureInfo.CurrentUICulture) +": " + row.Comment);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}