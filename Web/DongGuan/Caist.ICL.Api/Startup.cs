using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Caist.ICL.Core.Dao;

namespace Caist.ICL.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }


        public IConfiguration Configuration { get; }



        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var basePath = PlatformServices.Default.Application.ApplicationBasePath;
            services.AddSession();
            services.AddAuthentication("Bearer")
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateActor = false,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Configuration["Settings:AuthKey"]))
                };
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("api", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "输电电缆敷设参数现场测量技术研究"
                });

                new List<string>
                {
                    "Caist.ICL.Api.xml",
                    "Caist.ICL.Core.xml"
                }
                .ForEach(s => options.IncludeXmlComments(Path.Combine(basePath, s), true));
                options.DescribeAllEnumsAsStrings();
                options.OperationFilter<SwaggerParameterFilter>();
            });

            services.AddCors();
            services.AddMvc()
                .AddMvcOptions(options =>
                {
                    options.Filters.Add<ApiAuthorizationFilter>();
                })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore; 
                    options.SerializerSettings.MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore;
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                    options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                });
            //创建注册主键的builder
            ContainerBuilder builder = new ContainerBuilder();
            //将services中服务填充到Actofac中
            builder.Populate(services);

            var assembly = Assembly.LoadFrom(Path.Combine(basePath, "Caist.ICL.Data.dll"));
            //根据反射类型注册组件，暴露服务
            builder.RegisterAssemblyTypes(assembly)
                        .Where(type => type.Name == "UnitOfWork")
                        .As<IUnitOfWork>()
                        .InstancePerLifetimeScope();
            //注册当前程序集中以"Service"结尾的类
            assembly = Assembly.LoadFrom(Path.Combine(basePath, "Caist.ICL.Services.dll"));
            builder.RegisterAssemblyTypes(assembly)
                .Where(type => type.Name.EndsWith("Service"))
                .InstancePerDependency();

            var container = builder.Build();
            container.Resolve<IUnitOfWork>().Register(Configuration.GetConnectionString("DefaultConnection"));
            return new AutofacServiceProvider(container);

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSession();
            app.UseStaticFiles();
            app.UseCors(options =>
            {
                options.AllowAnyHeader();
                options.AllowAnyMethod();
                options.AllowAnyOrigin();
            });
            app.UseAuthentication();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.DefaultModelsExpandDepth(-1);
                options.RoutePrefix = "help";
                options.DocumentTitle = "输电电缆敷设参数现场测量技术研究API";
                options.SwaggerEndpoint("/swagger/api/swagger.json", "输电电缆敷设参数现场测量技术研究API");
            });
        }
    }
}
