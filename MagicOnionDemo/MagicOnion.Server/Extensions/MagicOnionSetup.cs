using Grpc.Core;
using MagicOnion.Server;
using MagicOnion.Server.Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MagicOnion.Server.Extensions
{
    /// <summary>
    /// MagicOnion-grpc 启动
    /// 源代码：https://github.com/Cysharp/MagicOnion
    /// 案例： https://www.cnblogs.com/NMSLanX/p/8242105.html
    /// </summary>
    public static class MagicOnionSetup
    {
        /// <summary>
        /// 添加MagicOnion框架启动项
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblyServices">相关服务类的反射集合</param>
        /// <param name="basePath">根目录路径</param>
        /// <param name="modelsXml">XML注释名</param>        
        public static void AddMagicOnionSetup(this IServiceCollection services, Assembly[] assemblyServices, string basePath, string modelsXml)
        {
            ////注意： 需要将所有的service类进行反射（新增一个，加一个）
            //Assembly[] assemblyServices = new Assembly[] {
            // typeof(TestServices).Assembly,
            //};

            string ipStr = Appsettings.GetSettingNode(new string[] { "Grpc", "Ip" });
            string portStr = Appsettings.GetSettingNode(new string[] { "Grpc", "Port" });

            if (assemblyServices.Length == 0) return;
            // 通过反射去拿
            MagicOnionServiceDefinition service = MagicOnionEngine.BuildServerServiceDefinition(
                // 加载引用程序集             
                assemblyServices,
                new MagicOnionOptions(true)
                {
                    MagicOnionLogger = new MagicOnionLogToGrpcLogger()
                });

            if (string.IsNullOrEmpty(ipStr) || string.IsNullOrEmpty(portStr))
                throw new Exception("appsettings 中没有对 grpc的ip或端口没有进行相关的配置！");

            Grpc.Core.Server server = new Grpc.Core.Server
            {
                Services = { service },
                Ports = { new ServerPort(ipStr, int.Parse(portStr), ServerCredentials.Insecure) }
            };
            server.Start();
            //注册要通过反射创建的组件
            //var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
            //services.AddSwaggerGen(c =>
            //{
            //    var filePath = Path.Combine(basePath, modelsXml);
            //    c.IncludeXmlComments(filePath);
            //});

            //这里添加服务
            services.Add(new ServiceDescriptor(typeof(MagicOnionServiceDefinition), service));
        }
    }
}
