using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace RMDP.Events {
    /// <summary>
    /// RMDP 消息事件基础类型
    /// </summary>
    /// <typeparam name="TMsg"></typeparam>
    [DataContract]
    public abstract class MsgEventBase<TMsg> : IRmdpEvent<TMsg> where TMsg : IMsgPackage {

        protected MsgEventBase () {
            EventId = Guid.NewGuid ();
        }

        protected MsgEventBase (TMsg msg) : this () {
            this.Body = msg;
            this.EventCreationDate = DateTime.UtcNow;
        }

        /// <summary>
        /// 消息Id
        /// </summary>
        /// <value></value>
        [JsonPropertyName ("id")]
        [DataMember (Name = "id", IsRequired = true)]
        public Guid EventId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        /// <value></value>
        [JsonPropertyName ("creation")]
        [DataMember (Name = "creation", IsRequired = true)]
        public DateTime EventCreationDate { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        /// <value></value>        
        [JsonPropertyName ("body")]
        [DataMember (Name = "body", IsRequired = true)]
        public TMsg Body { get; set; }

    }
}