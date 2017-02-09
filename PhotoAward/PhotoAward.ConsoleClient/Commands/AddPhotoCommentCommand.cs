using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PhotoAward.PhotoManagement.Interfaces;

namespace PhotoAward.ConsoleClient.Commands
{
    public class AddPhotoCommentCommand : Command
    {
        public override async Task ExecuteAsync(string[] args)
        {
            try
            {
                var photoId = new Guid(args[0]);
                var email = args[1];
                var comment = args[2];
                
                var commentUploadData = new CommentUploadData() { Email = email, PhotoId = photoId, Comment = comment, CreateDate = DateTime.Now};
                var client = new HttpClient();
                var result = await client.PostAsJsonAsync(BaseUrl + "/Photo/AddComment", commentUploadData);
                await WriteResultAsync(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public override string GetArguments()
        {
            var sb = new StringBuilder();
            sb.Append("idOfPhoto emailOfAuthor comment");
            return sb.ToString();
        }

        public override string GetDescription()
        {
            return "Fügt zum Photo mit der Id photoId einen Kommentar hinzu. Dem Author author wird der Kommentar zugeordnet.";
        }
    }
}