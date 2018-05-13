using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Feature.FormsExtensions.Business.ReCaptcha
{
    public class ReCaptchaService : IReCaptchaService
    {
        private readonly HttpClient client = new HttpClient();
        private readonly string secret;

        public ReCaptchaService(string secret)
        {
            this.secret = secret;
        }

        public async Task<bool> Verify(string response)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("secret", secret),
                new KeyValuePair<string, string>("response", response)
            });
            var responseMessage = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", formContent);
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            var reCaptchaResponse = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<ReCaptchaResponse>(jsonString));

            return reCaptchaResponse.Success;
        }

        public bool VerifySync(string response)
        {
            return AsyncHelpers.RunSync(() => Verify(response));
        }
    }
}