using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SantafeApi.Contracts.V1.External;
using SantafeApi.Options;
using SantafeApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SantafeApi.Services
{
    public class FacebookAuthService : IFacebookAuthService
    {
        private readonly string GetAcessTokenUrl = "https://graph.facebook.com/me?fields=first_name,last_name,email,picture&access_token={0}";
        private readonly string ValidateUrl = "https://graph.facebook.com/debug_token?input_token={0}&access_token={1}|{2}";
        private readonly FacebookAuthSettings _facebookAuthSettings;
        private readonly IHttpClientFactory _httpClientFactory;

        public FacebookAuthService(IOptions<FacebookAuthSettings> facebookAuthSettings, IHttpClientFactory httpClientFactory)
        {
            _facebookAuthSettings = facebookAuthSettings.Value;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<FacebookTokenValidationResult> ValidateAccessTokenAsync(string accessToken)
        {
            var formattedUrl = String.Format(ValidateUrl, accessToken, _facebookAuthSettings.AppId, _facebookAuthSettings.AppSecret);
            var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);
            result.EnsureSuccessStatusCode();
            var responseAsString = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<FacebookTokenValidationResult>(responseAsString);
        }
        public async Task<FacebookUserResult> GetUserAsync(string accessToken)
        {
            var formattedUrl = String.Format(GetAcessTokenUrl, accessToken);
            var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);
            result.EnsureSuccessStatusCode();
            var responseAsString = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<FacebookUserResult>(responseAsString);
        }
    }
}
