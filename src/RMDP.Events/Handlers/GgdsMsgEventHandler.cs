#if NETSTANDARD_21
using System.Threading.Tasks;
using Enbiso.NLib.EventBus;
using RMDP.Events;
using RMDP.Packages;

namespace RMDP.Handlers
{
    internal class GgdsMsgEventHandler : EventHandler<GgdsMsgEvent> {
        private readonly IRmdpEventClient<GgdsMsg> _client;

        public GgdsMsgEventHandler (IRmdpEventClient<GgdsMsg> client) {
            this._client = client;
        }

        protected override Task Handle (GgdsMsgEvent @event) {
            return this._client.Handle (@event.Body);
        }
    }
}
#endif