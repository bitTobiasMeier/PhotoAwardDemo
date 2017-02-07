using System;
using System.Net.Http;
using System.Threading.Tasks;
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

                var member = new PhotoUploadData() {Email = email, FileName  = filename, Title = title, Data = data };
                var client = new HttpClient();
                var result = await client.PostAsJsonAsync(BaseUrl + "/Photo/Add", member);
                await WriteResultAsync(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}