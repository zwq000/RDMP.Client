using System;
using System.Threading.Tasks;
using Enbiso.NLib.EventBus;
using Enbiso.NLib.EventBus.Nats;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NATS.Client;
using RMDP.Packages;
using Xunit;

namespace RMDP.Events.Tests {
    public class NLibEventBusPublishTests {
        private NatsOptions natsOptions = new NatsOptions () {
            Servers = new string[] { "nats://192.168.1.22:4222" },
            Exchanges = new string[] { "rdmp" }
        };

        ILoggerFactory factory = LoggerFactory.Create(cfg=>{cfg.AddConsole();});
        

        [Fact]
        public async Task TestPblishAsync () {
            //var publisher = CreatePublisher();
            var publisher = CreatePublisher ();
            //Assert.Equal (ConnState.CONNECTED, conn.State);
            for (int i = 0; i < 1000; i++) {
                var e = new GgdsMsgEvent (new GgdsMsg (i) { SDT = DateTime.Now });
                await publisher.Publish (e);
                //await Task.Delay (100);
            }
        }

        IEventPublisher CreatePublisher () {
            var options = new Options (natsOptions);
            return new NatsEventPublisher (options, CreateConnection (options), factory.CreateLogger<NatsEventPublisher>());
        }

        private class Options : IOptions<NatsOptions> {
            public Options (NatsOptions opt) {
                this.Value = opt;
            }
            public NatsOptions Value { get; }
        }
        private INatsConnection CreateConnection (Options opt) {
            const string url = "nats://192.168.1.22:4222";
            var opts = ConnectionFactory.GetDefaultOptions ();
            opts.Url = url;
            return new NatsConnection (new ConnectionFactory (), opt, factory.CreateLogger<NatsConnection>());
        }
    }

}