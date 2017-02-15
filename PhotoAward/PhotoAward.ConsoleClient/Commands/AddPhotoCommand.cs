using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Converters;
using PhotoAward.PhotoManagement.Interfaces;

namespace PhotoAward.ConsoleClient.Commands
{
    public class AddPhotoCommand : Command
    {
        public override async Task ExecuteAsync(string[] args)
        {
            try
            {
                var email = args[0];
                var filename = args[1];
                var title = args[2];

                var data = System.IO.File.ReadAllBytes(filename);

                var photoUploadData = new PhotoUploadData() {Email = email, FileName  = filename, Title = title, Data = data };
                var client = new HttpClient();
                var result = await client.PostAsJsonAsync(BaseUrl + "/Photo/Add", photoUploadData);
                var datetimeconverter = new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-ddHH:mm:ss" };
                var mngmntdata = Newtonsoft.Json.JsonConvert.DeserializeObject<PhotoManagementData>(await result.Content.ReadAsStringAsync(), datetimeconverter);
                Console.WriteLine("Photo Id: " + mngmntdata.Id);
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
            return "Ordnet dem Mitglied mit der Emailadresse email das angegebene Photo mit dem übergebenen Titel zu";
        }
    }

    public class DeletePhotoCommand : Command
    {
        public override async Task ExecuteAsync(string[] args)
        {
            try
            {
                var photoId = args[0];
                var client = new HttpClient();
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
            return "Ordnet dem Mitglied mit der Emailadresse email das angegebene Photo mit dem übergebenen Titel zu";
        }
    }
}