using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Pokemon.API;

namespace Pokemon.Tests.IntegrationTest
{
    /// <summary>
    /// Integration Test Web Factory to setup the Configuration and WebHost
    /// </summary>
    public abstract class TestWebFactory : WebApplicationFactory<Startup>
    {
        private readonly IConfiguration _configuration;

        protected HttpClient TestHttpClient => CreateClient();

        protected TestWebFactory()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("IntegrationTest/appsettings.Integration.json")
                .Build();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services => { })
                .UseEnvironment("Integration")
                .ConfigureAppConfiguration(config =>
                {
                    config.AddConfiguration(_configuration);
                });
        }
    }
}
