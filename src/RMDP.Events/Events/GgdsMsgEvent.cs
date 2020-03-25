using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using RMDP.Packages;

namespace RMDP.Events {
    /// <summary>
    /// 定位数据上传消息事件
    /// </summary>
    [DataContract]
    public class GgdsMsgEvent : MsgEventBase<GgdsMsg> {

        public GgdsMsgEvent () : base () { }

        public GgdsMsgEvent (GgdsMsg msg) : base (msg) { }
    }
}