using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Pokemon.Entities;
using Pokemon.Services.Interfaces;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Pokemon.Services
{
    public class TranslateService : ITranslateService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public TranslateService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<string> GetShakespeareText(string inputText)
        {
            TranslationResponse transltResp=null;
            var translateClient = _httpClientFactory.CreateClient("TranslationAPIConnection");
            inputText = inputText.Replace("\n", " ").Replace("\r"," ");
            string text = HttpUtility.UrlEncode(inputText);
            var response = await translateClient.GetAsync(_configuration["TranslationApi:ShakespeareEnglishURL"] +
                $"{text}");
            if (response.IsSuccessStatusCode)
            {
                transltResp = JsonConvert.DeserializeObject<TranslationResponse>(
                    await response.Content.ReadAsStringAsync());
            }
            else
            {
                if (response.StatusCode == (HttpStatusCode)429)
                    throw new APIException((HttpStatusCode)429, "No Shakespeare Translation Available as this time. Please try again later");
                throw new Exception("Some Error Occured");
            }
            return transltResp?.Contents?.Translated;
        }
    }
}
