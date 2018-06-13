using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace WebAPI.SwaggerForASPDotNetCore
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
            //处理跨域访问的问题
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            services.AddSwaggerGen(options =>
            {
                var uriFriendlyName = "your_uri_name";
                options.SwaggerDoc(uriFriendlyName, new Info
                {
                    Description = "this is your descprtion",
                    Version = "v1",
                    Title = "zmphone API [" + DateTime.Now.ToString() + "]"
                });

                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, string.Concat(this.GetType().Namespace,".xml"));
                //指定 生成的web api项目的xml描述文件 的路径
                options.IncludeXmlComments(xmlPath);
            });
            services.AddMvc().AddJsonOptions(Options => Options.SerializerSettings.ContractResolver = new DefaultContractResolver());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            #region ms logging
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            #endregion
            /*
             * If you want to use other logger(ex. Nlog,Log4Net) 
             * Please reference them by manuall mode in  NuGet Package Manager.
             * 依赖注入（DI）
             * PS:请注意注入的顺序（顺序不对，启动时会受影响）
             */ 

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //#region 处理异常
            //app.UseExceptionHandler("/Home/Error"); // Call first to catch exceptions
            //                                        // thrown in the following middleware.

            //app.UseStaticFiles();                   // Return static files and end pipeline.

            //app.UseAuthentication();               // Authenticate before you access
            //                                       // secure resources.

            //app.UseMvcWithDefaultRoute();          // Add MVC to the request pipeline.
            //#endregion
            app.UseMvc();
            app.UseMvcWithDefaultRoute();

            app.UseSwagger();
            app.UseSwaggerUI(c=> { c.SwaggerEndpoint("/swagger/your_uri_name/swagger.json", "my web api v1");  });
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "my web api V1");
            //});

        }
    }
}
