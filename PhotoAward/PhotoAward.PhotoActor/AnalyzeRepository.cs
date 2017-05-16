using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoAward.PhotoActors
{
    public interface IAnalyzeRepository
    {
        Task<string> AnalyzeImageAsync(byte [] imagesBytes);
    }

    public class AnalyzeRepository: IAnalyzeRepository
    {
        public async Task<string> AnalyzeImageAsync(byte [] imagesBytes)
        {
            var client = new HttpClient();

            // Request headers - replace this example key with your valid subscription key.
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "09d57dec054f438093f7a5c3a6c0db34");

            // Request parameters. A third optional parameter is "details".
            string requestParameters = "visualFeatures=Categories&language=en";
            string uri = "https://westeurope.api.cognitive.microsoft.com/vision/v1.0/describe?" + requestParameters;
            Console.WriteLine(uri);

     
            using (var content = new ByteArrayContent(imagesBytes))
            {
                // This example uses content type "application/octet-stream".
                // The other content types you can use are "application/json" and "multipart/form-data".
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                var response = await client.PostAsync(uri, content);
                var resulttxt = await response.Content.ReadAsStringAsync();
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<JsonImageInfo>(resulttxt);
                var description = GetDescription(result);
                return description;
            }

        }

        private static string GetDescription(JsonImageInfo result)
        {
            if (result?.description?.captions == null) return String.Empty;
            
            var sb = new StringBuilder();
            foreach (var caption in result.description.captions)
            {
                if (caption == null) continue;
                sb.AppendLine(caption.text);
            }
            return sb.ToString();
        }
    }


    public class JsonImageInfo
    {
        public JsonDescription description;
    }

    public class JsonDescription
    {
        public List<JsonText> captions;
    }

    public class JsonText
    {
        public string text;
    }
}
