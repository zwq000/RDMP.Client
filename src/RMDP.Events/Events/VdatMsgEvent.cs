using System.Runtime.Serialization;
using RMDP.Packages;

namespace RMDP.Events {
    /// <summary>
    /// 行车数据（车辆采集器数据）上传消息 - VDAT
    /// </summary>
    [DataContract]
    public class VdatMsgEvent : MsgEventBase<VdatMsg> {
        public VdatMsgEvent () : base () {

        }

        public VdatMsgEvent (VdatMsg msg) : base (msg) {

        }
    }
}