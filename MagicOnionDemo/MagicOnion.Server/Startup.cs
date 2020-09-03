using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MagicOnion.Server.Extensions;
using MagicOnion.Server.Middlewares;

namespace MagicOnion.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        static string basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //注意： 需要将所有的service类进行反射
            Assembly[] assemblyServices = new Assembly[] {
                //typeof(TestServices).Assembly,
            };
            string modelXml = "HC.User.Grpc.Proxy.xml";
            var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);

            //里面添加swagger的注册
            services.AddMagicOnionSetup(assemblyServices, basePath, modelXml);


            services.AddControllers();
        }

        /// <summary>
        /// Autofac注册，注意在Program.CreateHostBuilder，添加Autofac服务工厂
        /// </summary>
        /// <param name="builder"></param>
        //public void ConfigureContainer(ContainerBuilder builder)
        //{
        //    builder.RegisterModule(new AutofacModuleRegister());
        //    //ContainerManager.SetContainer(builder.Build());
        //}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMagicOnionMilddle(basePath, "HC.User.Grpc.Proxy.xml");
            //app.UseSwagger();
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API-v1");
            //    c.RoutePrefix = string.Empty;
            //});

            app.UseRouting();

            //app.UseAuthorization();

            //var autofacRoot = app.ApplicationServices.GetAutofacRoot();
            //AutofacRoot.SetContainer(autofacRoot);
            //var repository = autofacRoot.Resolve<IOrderServices>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
