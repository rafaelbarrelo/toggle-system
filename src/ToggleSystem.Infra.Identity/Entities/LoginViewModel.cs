using Newtonsoft.Json;

namespace ToggleSystem.Infra.Identity.Entities
{
    public class LoginViewModel
    {
        [JsonProperty(PropertyName = "user")]
        public string User { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }
}
