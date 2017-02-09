using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Converters;
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
                var client = new HttpClient();
                var result = await client.GetAsync(BaseUrl + "/Photo/GetComments/" + photoId.ToString());
                var datetimeconverter = new IsoDateTimeConverter() {DateTimeFormat = "yyyy-MM-ddHH:mm:ss"};
                var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CommentData>>(await result.Content.ReadAsStringAsync(), datetimeconverter);
                foreach (var row in list)
                {
                    Console.WriteLine(row.Comment);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}