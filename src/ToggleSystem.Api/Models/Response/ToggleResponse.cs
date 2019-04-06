using Newtonsoft.Json;

namespace ToggleSystem.Api.Models.Response
{
    public class ToggleResponse
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "version")]
        public int Version { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
