using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PhotoAward.ConsoleClient.Commands
{
    public class LoginCommand : Command
    {
        
        public override async Task ExecuteAsync(string[] args)
        {
            try
            {
                var email = args[0];
                var password = args[1];
                var client = new HttpClient();
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("username", email),
                    new KeyValuePair<string, string>("password", password),
                    new KeyValuePair<string, string>("grant_type", "password")
                });
                var url = this.BaseUrl.Replace("/api", "/token");
                var result = await client.PostAsync(url, content);
                await WriteResultAsync(result);
                var token = JsonConvert.DeserializeObject<BearerToken>(await result.Content.ReadAsStringAsync());
                System.IO.File.WriteAllText("bearer.token",token.access_token);

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
            return "Meldet den Benutzer an";
        }
    }

    internal class BearerToken
    {
        public string access_token;
        public string token_type;
        public string expires_in;
    }
}