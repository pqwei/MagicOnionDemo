using MagicOnion.Common.IService;
using MagicOnion.Common.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagicOnion.Server.Core
{
    public class TestService : ServiceBase<ITestService>, ITestService
    {
        public UnaryResult<TestResponse> GetStudent(int sid)
        {
            Student student;
            student.Name = "Test_小明";
            student.Sid = sid;

            //FillResult
            TestResponse result = new TestResponse()
            {
                Data = student,
                Status = 0,
                Msg = "GRPC成功！"
            };

            //Return
            return UnaryResult(result);
        }
    }
}
