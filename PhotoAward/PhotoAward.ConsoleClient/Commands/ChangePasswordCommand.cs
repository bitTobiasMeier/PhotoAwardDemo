using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PhotoAward.MemberManagement.Interfaces;

namespace PhotoAward.ConsoleClient.Commands
{
    public class ChangePasswordCommand : Command
    {
        public override async Task ExecuteAsync(string[] args)
        {
            try
            {
                var member = new ChangePasswordDto() { Email = args[0], OldPassword = args[1], NewPassword = args[2] };
                var client = new HttpClient();
                var result = await client.PostAsJsonAsync(BaseUrl + "/Member/ChangePassword", member);
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
            sb.Append("email passwort neuesPasswort");
            return sb.ToString();
        }

        public override string GetDescription()
        {
            return "Legt ein neues Mitglied an.";
        }
    }
}