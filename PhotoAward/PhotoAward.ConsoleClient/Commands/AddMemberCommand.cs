using System;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using PhotoAward.MemberManagement.Interfaces;

namespace PhotoAward.ConsoleClient.Commands
{
    public class AddMemberCommand : Command
    {
        public override async Task ExecuteAsync(string[] args)
        {
            try
            {
                var member = new MemberDto() {Email = args[0], FirstName = args[1], Surname = args[2]};
                var client = new HttpClient();
                var result = await client.PostAsJsonAsync(BaseUrl + "/Member/Add", member);
                await WriteResultAsync(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}