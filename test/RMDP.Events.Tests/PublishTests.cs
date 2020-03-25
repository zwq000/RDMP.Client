using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NATS.Client;
using Newtonsoft.Json;
using RMDP.Packages;
using Xunit;

namespace RMDP.Events.Tests {
    public class PublishTests {
        [Fact]
        public async Task TestPublishAsync () {
            const string subject = "rdmp.GgdsMsgEvent";
            using (var conn = CreateConnection ()) {
                Assert.Equal (ConnState.CONNECTED, conn.State);
                for (int i = 0; i < 10; i++) {
                    var e = new GgdsMsgEvent (new GgdsMsg (i) { SDT = DateTime.Now });
                    conn.Publish (subject, System.Text.Json.JsonSerializer.SerializeToUtf8Bytes (e));
                    conn.Flush ();
                    await Task.Delay (100);
                }
            }
        }

        private IConnection CreateConnection () {
            const string url = "nats://192.168.1.22:4222";
            var opts = ConnectionFactory.GetDefaultOptions ();
            opts.Url = url;
            opts.User="guest";
            opts.Password="guest";
            return new ConnectionFactory ().CreateConnection (opts);
        }
    }

    internal static class EventMock {

        public static IEnumerable<GgdsMsg> GenGgdsMsg (int count = 100) {
            for (var i = 0; i < count; i++) {
                yield return new GgdsMsg (i){
                    SDT = DateTime.Now
                };
            }
        }
    }

}