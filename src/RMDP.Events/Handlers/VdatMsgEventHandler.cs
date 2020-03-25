using System.Threading.Tasks;
#if NETSTANDARD_21
using Enbiso.NLib.EventBus;
using RMDP.Events;
using RMDP.Packages;

namespace RMDP.Handlers
{
    internal class VdatMsgEventHandler : EventHandler<VdatMsgEvent> {
        private readonly IRmdpEventClient<VdatMsg> _client;

        public VdatMsgEventHandler (IRmdpEventClient<VdatMsg> client) {
            this._client = client;
        }

        protected override Task Handle (VdatMsgEvent @event) {
            return this._client.Handle (@event.Body);
        }
    }
}
#endif