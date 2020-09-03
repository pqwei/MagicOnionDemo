using MagicOnion.Common.Model.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace MagicOnion.Common.IService
{
    public interface ITestService : IService<ITestService>
    {
        /// <summary>
        /// 获取一个活的学生
        /// </summary>
        /// <param name="sid">学生id</param>
        /// <returns>一个鲜活的学生</returns>
        UnaryResult<TestResponse> GetStudent(int sid);
    }

}
