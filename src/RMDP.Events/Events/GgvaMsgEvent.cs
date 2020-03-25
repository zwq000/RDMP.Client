using System.Runtime.Serialization;
using RMDP.Packages;

namespace RMDP.Events
{
    /// <summary>
    /// 车辆超速报警事件 GGVA
    /// </summary>
    [DataContract]
    public class GgvaMsgEvent : MsgEventBase<GgvaMsg> {
        public GgvaMsgEvent () : base () {

        }

        public GgvaMsgEvent (GgvaMsg msg) : base (msg) {

        }
    }
}