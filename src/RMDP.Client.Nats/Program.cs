using System;
using System.Text;
using System.Text.Json;
using CommandLine;
using NATS.Client;
using RMDP.Events;

namespace RMDP.Client.Nats {
    class Program {
        public static void Main (string[] args) {
            Parser.Default.ParseArguments<CommandOptions> (args)
                .WithParsed<CommandOptions> (o => {
                    Console.WriteLine("start nats Subscribe from {0} subject:{1}",o.Url,o.Subject);
                    var subscriber = new Subscriber (o.ToNatsOptions ());
                    subscriber.Subscribe (nameof (GgdsMsgEvent), OnRecGgdsMsg);
                    subscriber.Subscribe (nameof (LinkStateEvent), OnLinkStateChanged);
                    subscriber.Subscribe (nameof (VdatMsgEvent), onRecVdatMsg);
                });
        }

        private static void onRecVdatMsg (object sender, MsgHandlerEventArgs args) {
            var message = Encoding.UTF8.GetString (args.Message.Data);
            var e = (LinkStateEvent) JsonSerializer.Deserialize (message, typeof (LinkStateEvent));
            var state = e.Connected? "连接": "断开";
            Console.WriteLine ($"{e.SSN} {state}");
        }

        private static void OnLinkStateChanged (object sender, MsgHandlerEventArgs args) {
            var message = Encoding.UTF8.GetString (args.Message.Data);
            var e = (VdatMsgEvent) JsonSerializer.Deserialize (message, typeof (VdatMsgEvent));
            Console.WriteLine (JsonSerializer.Serialize (e.Body));
        }

        static void OnRecGgdsMsg (object sender, MsgHandlerEventArgs args) {
            var message = Encoding.UTF8.GetString (args.Message.Data);
            var e = (GgdsMsgEvent) JsonSerializer.Deserialize (message, typeof (GgdsMsgEvent));
            var msg = e.Body;
            Console.WriteLine ($"VNB:{msg.VNB} Speed:{msg.SPD} [{msg.LGT:N4},{msg.LTT:N4}] CID:{msg.CID} ");
        }
    }
}