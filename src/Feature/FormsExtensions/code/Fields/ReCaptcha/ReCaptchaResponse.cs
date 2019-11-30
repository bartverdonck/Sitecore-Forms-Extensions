using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Feature.FormsExtensions.Fields.ReCaptcha
{
    public class ReCaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("challenge_ts")]
        public DateTime ChallengeTimestamp { get; set; }
        [JsonProperty("hostname")]
        public string Hostname { get; set; }
        [JsonProperty("error-codes")]
        public IList<string> ErrorCodes { get; set; }
    }
}