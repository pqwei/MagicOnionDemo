using MagicOnion;
using MagicOnion.Server;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Grpc.Core;
using MagicOnion.Server.Common;
using MagicOnion.Server.Extensions;

namespace MagicOnion.Server.Middlewares
{
    public static class MagicOnionMilddle
    {
        public static void UseMagicOnionMilddle(this IApplicationBuilder app, string basePath, string modelXml)
        {
            string ipStr = Appsettings.GetSettingNode(new string[] { "Grpc", "Ip" });
            string portStr = Appsettings.GetSettingNode(new string[] { "Grpc", "Port" });

            if (string.IsNullOrEmpty(ipStr) || string.IsNullOrEmpty(portStr))
                throw new Exception("appsettings 中没有对 grpc的ip或端口没有进行相关的配置！");

            //获取添加了的服务
            var magicOnion = app.ApplicationServices.GetService<MagicOnionServiceDefinition>();
            //使用MagicOnion的Swagger扩展，就是让你的rpc接口也能在swagger页面上显示
            //下面这些东西你可能乍一看就懵逼，但你看到页面的时候就会发现，一个萝卜一个坑。
            //注意：swagger原生用法属性都是大写的，这里是小写。
            //var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);

            var ApiName = Appsettings.GetSettingNode(new string[] { "Startup", "ApiName" });


            //要想让rpc成为该web服务的接口，流量和协议被统一到你写的这个web项目中来，那么就要用个方法链接你和rpc
            //这个web项目承接你的请求，然后web去调用rpc获取结果，再返回给你。
            //因此需要下面这句话      
            //app.UseMagicOnionHttpGateway(magicOnion.MethodHandlers, new Channel($"{ipStr}:{portStr}", ChannelCredentials.Insecure));
        }

    }
}
