using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CacheManager.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace ServiceProxy
{
   public class Startup
   {
      private IConfigurationRoot Configuration { get; }
      public Startup(IHostingEnvironment env)
      {
         var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
         .SetBasePath(env.ContentRootPath)
         .AddJsonFile("configuration.json", optional: false)
         .AddEnvironmentVariables();

         Configuration = builder.Build();
      }


      // This method gets called by the runtime. Use this method to add services to the container.
      // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
      public void ConfigureServices(IServiceCollection services)
      {

         services.AddOcelot(Configuration);
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
      {
         loggerFactory.AddConsole(Configuration.GetSection("Logging"));
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }
         app.UseOcelot().Wait();
      }
   }
}