using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using NUnit.Framework;
using Pokemon.Entities;
using Pokemon.Services;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Pokemon.Tests.Services
{
    public class TranslationServiceTest
    {
        Mock<IHttpClientFactory> httpClientMock;
        Mock<IConfiguration> configMock;
        Mock<HttpMessageHandler> httpMessageHandlerMock;

        [SetUp]
        public void Setup()
        {
            httpClientMock = new Mock<IHttpClientFactory>();
            configMock = new Mock<IConfiguration>();
            httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        }
        [Test]
        public void TestTranslation()
        {
            
            var translateResp = new TranslationResponse()
            {
                Contents = new Contents()
                {
                    Text = "Sample Text",
                    Translated = "Sample Translated Text",
                    Translation = "Sample Text to be Translated"
                }
            };
            // Mocking the response of HTTP Client Factory
            // Both GetAsync and PostAysnc Internally use SendAsync
            httpMessageHandlerMock.Protected().Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                   ItExpr.IsAny<CancellationToken>()
    
               )
               // prepare the expected response of the mocked http call
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(JsonConvert.SerializeObject(translateResp))
               });

            // Given a Translation Client with sample input
            httpClientMock.Setup(obj => obj.CreateClient(It.IsAny<string>())).Returns(new HttpClient(httpMessageHandlerMock.Object));

            // When Translation Service is called
            var transObj = new TranslateService(httpClientMock.Object, configMock.Object);

            // Then Translated Text is returned
            Assert.IsNotNull(transObj.GetShakespeareText("some string to translate"));
        }

    }
}