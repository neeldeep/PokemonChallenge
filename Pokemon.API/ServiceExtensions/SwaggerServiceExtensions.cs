using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace Pokemon.API.ServiceExtensions
{

    public static class SwaggerServiceExtensions
    {
        /// <summary>
        /// This enables the Swagger on the Pokemon API.
        /// </summary>
        /// <param name="services"></param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Version = "v1.0", Title = "Pokemon Challenge API", Description = "This API is used for Pokemon Functionality" });
                // Set the comments path for the Swagger 
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
          

            return services;
        }
        /// <summary>
        /// Setup Swagger Path
        /// </summary>
        /// <param name="app"></param>
        /// <returns>IApplicationBuilder</returns>
        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pokemon Challenge API V1");
            });

            return app;
        }
    }
}
