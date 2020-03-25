using System;
using Enbiso.NLib.EventBus;

namespace RMDP {
    public interface IRmdpEvent<TMsg> : IEvent where TMsg : IMsgPackage {

        /// <summary>
        /// 消息内容
        /// </summary>
        /// <value></value>
        TMsg Body { get; set; }
    }
}

