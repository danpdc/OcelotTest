using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceProxy
{
   public class Program
   {
      public static void Main(string[] args)
      {
         {

            IWebHostBuilder builder = new WebHostBuilder();

            builder.ConfigureServices(s =>
            {
               s.AddSingleton(builder);
            });

            builder.UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>();

            var host = builder.Build();

            host.Run();
         }
      }
   }
}
