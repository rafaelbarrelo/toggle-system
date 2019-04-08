using Newtonsoft.Json;

namespace ToggleSystem.Infra.Identity.Entities
{
    public class ApiToken
    {
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }
        [JsonProperty(PropertyName = "expires_on")]
        public int ExpiresOn { get; set; }
    }
}
