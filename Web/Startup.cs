using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using ES.Business.Service;
using ES.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NLog.Extensions.Logging;
namespace ES.Web
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
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);
            services.AddHttpClient();
            #region ESÅäÖÃ
            services.AddEsService(options =>
            {
                options.Urls = Configuration["ElasticSearch:Uri"];
                options.UserName = Configuration["ElasticSearch:UserName"];
                options.Password = Configuration["ElasticSearch:Password"];
            });
            #endregion
            #region swagger ÅäÖÃ
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "ES.Web", Version = "v1" });

                var basePath = AppContext.BaseDirectory;

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(basePath, xmlFile);
                options.IncludeXmlComments(xmlPath);

                var vcPath = Path.Combine(basePath, "ES.Business.xml");
                options.IncludeXmlComments(vcPath);

                var vcEntityPath = Path.Combine(basePath, "ES.Models.xml");
                options.IncludeXmlComments(vcEntityPath);

            });
            #endregion
            services.AddControllers();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            IocManager.Instance.Initialize(builder);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger().UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    //c.RoutePrefix = string.Empty;
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
#if DEBUG
            app.UseSwagger().UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                //c.RoutePrefix = string.Empty;
            });
#endif
            #region ÈÕÖ¾ÅäÖÃ
            loggerFactory.AddNLog();
            #endregion
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
