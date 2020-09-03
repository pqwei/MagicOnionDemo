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
        /// 添加MagicOnion启动项
        /// </summary>
        /// <param name="services"></param> 
        public static void AddMagicOnionSetup(this IServiceCollection services)
        {
            string ipStr = Appsettings.GetSettingNode(new string[] { "Grpc", "Ip" });
            string portStr = Appsettings.GetSettingNode(new string[] { "Grpc", "Port" });

            // 通过反射去拿
            MagicOnionServiceDefinition service = MagicOnionEngine.BuildServerServiceDefinition(
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
        }
    }
}
