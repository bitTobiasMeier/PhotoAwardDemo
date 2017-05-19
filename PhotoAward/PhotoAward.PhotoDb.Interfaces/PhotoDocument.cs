using Newtonsoft.Json;

namespace PhotoAward.PhotoDb.Interfaces
{
    public class PhotoDocument 
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "image")]
        public byte[] Image { get; set; }

       
    }
}