using Caist.Framework.Business.AutoJob;
using Caist.Framework.Util;
using Caist.Framework.Util.Model;
using Caist.Framework.WebApi.Handle;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

namespace Caist.Framework.WebApi
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new Info { Title = "ú��ȫ���ϵͳApi�ӿڷ���", Version = "v1" });
            });

            services.AddOptions();
            services.AddCors();
            services.AddMvc(options =>
            {
                options.ModelMetadataDetailsProviders.Add(new ModelBindingMetadataProvider());
            }).AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(GlobalContext.HostingEnvironment.ContentRootPath + Path.DirectorySeparatorChar + "DataProtection"));
            services.AddCors(options => options.AddPolicy("Domain",
            builder => builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().AllowCredentials()));
            GlobalContext.SystemConfig = Configuration.GetSection("SystemConfig").Get<SystemConfig>();
            GlobalContext.Services = services;
            GlobalContext.ServiceProvider = services.BuildServiceProvider();
            GlobalContext.Configuration = Configuration;
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                GlobalContext.SystemConfig.Debug = true;
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseDeveloperExceptionPage();
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

            app.UseMiddleware(typeof(GlobalExceptionMiddleware));

            app.UseCors(builder =>
            {
                builder.WithOrigins(GlobalContext.SystemConfig.AllowCorsSite.Split(',')).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
            });
            app.UseMvc();

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "api-doc/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "api-doc";
                c.SwaggerEndpoint("v1/swagger.json", "ú��ȫ���ϵͳApi�ӿڷ��� v1");
            });
            new JobCenter().Start(); // ��ʱ����
        }
    }
}
