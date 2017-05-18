using System;
using System.Net.Http;
using System.Text;
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
                var client = CreateClientWithAuthorizationHeader();
                var result = await client.GetAsync(BaseUrl + "/Member/Get/" + email);
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
            sb.Append("email");
            return sb.ToString();
        }

        public override string GetDescription()
        {
            return "Gibt die Detaildaten des Mitglieds mit der übergebenen Emailadresse aus.";
        }
    }


   
}