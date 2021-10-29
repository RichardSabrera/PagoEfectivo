using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WsApiEfectivo.BusinessLogic;

namespace WsApiEfectivo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped<IGenerateCode, GenerateCode>();
            services.AddScoped<IExchangeCode, ExchangeCode>();
            services.AddScoped<ISearchListCode, SearchListCode>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WsApiEfectivo", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WsApiEfectivo v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseExceptionHandler(
                           options =>
                           {
                               options.Run(async context =>
                               {
                                   context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                                   context.Response.ContentType = "text/html";
                                   var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();
                                   if (null != exceptionObject)
                                   {
                                       //var errorMessage = $"{exceptionObject.Error.Message}"; -- Se generaria log
                                       var errorMessage = "Ha ocurrido un error inesperado.";
                                       await context.Response.WriteAsync(errorMessage).ConfigureAwait(false);
                                   }
                               });
                           }
                       );
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
