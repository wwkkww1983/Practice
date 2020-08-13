﻿using Caist.Framework.Util;
using Caist.Framework.Util.Model;
using Caist.Framework.Web.Controllers;
using Caist.Framework.WebService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Caist.Framework.WebService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IConfigurationRoot ConfigurationRoot { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;

            GlobalContext.LogWhenStart(env);

            GlobalContext.HostingEnvironment = env;

            var builder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath)
                                                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                                                    .AddEnvironmentVariables();
            ConfigurationRoot = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, MQTTTaskService>();
            services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, PLCTaskService>();
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));

            services.AddMvc(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
                options.ModelMetadataDetailsProviders.Add(new ModelBindingMetadataProvider());
            });

            services.AddSession();
            services.AddHttpContextAccessor();

            services.AddOptions();

            services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(GlobalContext.HostingEnvironment.ContentRootPath + Path.DirectorySeparatorChar + "DataProtection"));

            GlobalContext.Services = services;
            GlobalContext.ServiceProvider = services.BuildServiceProvider();
            GlobalContext.Configuration = Configuration;
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            GlobalContext.SystemConfig = Configuration.GetSection("SystemConfig").Get<SystemConfig>();
            if (!string.IsNullOrEmpty(GlobalContext.SystemConfig.VirtualDirectory))
            {
                app.UsePathBase(new PathString(GlobalContext.SystemConfig.VirtualDirectory)); // 让 Pathbase 中间件成为第一个处理请求的中间件， 才能正确的模拟虚拟路径
            }
            string resource = Path.Combine(env.ContentRootPath, "Resource");
            if (!Directory.Exists(resource))
            {
                Directory.CreateDirectory(resource);
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = GlobalContext.SetCacheControl
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = "/Resource",
                FileProvider = new PhysicalFileProvider(resource),
                OnPrepareResponse = GlobalContext.SetCacheControl
            });
            
        }
    }
}
