using MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace MagicOnion.Common.Model.Response
{
    [MessagePackObject(true)]
    public class TestResponse
    {
        public string Msg;
        public int Status;
        public object Data;
    }

    [MessagePackObject(true)]
    public struct Student
    {
        public string Name;
        public int Sid;
    }
}
