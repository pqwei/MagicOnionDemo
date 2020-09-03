using MagicOnion.Common.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagicOnion.Server.Core
{
    public class TestService : ServiceBase<TestService>
    {
        public UnaryResult<TestResponse> GetStudent(int sid)
        {
            //GetStudentBySidFromDatabase(sid)
            //GetModel
            Student student;
            student.Name = "Test_小明";
            student.Sid = sid;

            //FillResult
            TestResponse result = new TestResponse()
            {
                Data = student,
                Status = 0
            };

            //Return
            return UnaryResult(result);
        }
    }
}
