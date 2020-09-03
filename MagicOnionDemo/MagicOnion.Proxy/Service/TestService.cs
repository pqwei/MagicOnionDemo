using Grpc.Core;
using MagicOnion.Client;
using MagicOnion.Common.IService;
using MagicOnion.Common.Model.Request;
using MagicOnion.Common.Model.Response;
using MagicOnion.Server;
using MagicOnion.Server.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MagicOnion.Proxy.Service
{
    public class TestService : ServiceBase<ITestService>, ITestService
    {
        private ITestService Client;

        public UnaryResult<TestResponse> GetStudent(TestRequest request)
        {
            return GetClient<ITestService>().GetStudent(request);
        }

        public ITestService GetClient<T>()
        {
            if (Client == null)
            {
                string ipStr = Appsettings.GetSettingNode(new string[] { "Grpc", "Ip" });
                int port = Appsettings.GetSettingNodeValue<int>(Convert.ToInt32, 0, new string[] { "Grpc", "Port" });

                if (string.IsNullOrEmpty(ipStr) || port == 0)
                    throw new Exception("appsettings 中没有对 grpc的ip或端口没有进行相关的配置！");
                var channel = new Channel("localhost", port, ChannelCredentials.Insecure);
                Client = MagicOnionClient.Create<ITestService>(channel);
            }

            return Client;
        }
    }
}
