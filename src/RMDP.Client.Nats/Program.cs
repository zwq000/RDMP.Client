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
                    Console.WriteLine ("start nats Subscribe from {0} subject:{1}", o.Url, o.Subject);
                    var subscriber = new Subscriber (o.ToNatsOptions ());
                    subscriber.Subscribe (nameof (GgdsMsgEvent), OnRecGgdsMsg);
                    subscriber.Subscribe (nameof (LinkStateEvent), OnLinkStateChanged);
                    subscriber.Subscribe (nameof (VdatMsgEvent), onRecVdatMsg);
                    subscriber.Subscribe (nameof (CobdMsgEvent), onRecCobdMsg);
                });
        }

        private static void onRecCobdMsg (object sender, MsgHandlerEventArgs args) {
            var message = Encoding.UTF8.GetString (args.Message.Data);
            var e = (CobdMsgEvent) JsonSerializer.Deserialize (message, typeof (CobdMsgEvent));
            Console.WriteLine ("OBD::" + JsonSerializer.Serialize (e.Body));
        }

        private static void onRecVdatMsg (object sender, MsgHandlerEventArgs args) {
            var message = Encoding.UTF8.GetString (args.Message.Data);
            var e = (LinkStateEvent) JsonSerializer.Deserialize (message, typeof (LinkStateEvent));
            var state = e.Connected? "连接": "断开";
            Console.WriteLine ($"LinkState::{e.SSN} {state}");
        }

        private static void OnLinkStateChanged (object sender, MsgHandlerEventArgs args) {
            var message = Encoding.UTF8.GetString (args.Message.Data);
            var e = (VdatMsgEvent) JsonSerializer.Deserialize (message, typeof (VdatMsgEvent));
            Console.WriteLine ("VDAT::"+JsonSerializer.Serialize (e.Body));
        }

        static void OnRecGgdsMsg (object sender, MsgHandlerEventArgs args) {
            var message = Encoding.UTF8.GetString (args.Message.Data);
            var e = (GgdsMsgEvent) JsonSerializer.Deserialize (message, typeof (GgdsMsgEvent));
            var msg = e.Body;
            Console.WriteLine ($"GGDS::VNB:{msg.VNB} Speed:{msg.SPD} [{msg.LGT:N4},{msg.LTT:N4}] CID:{msg.CID} ");
        }
    }
}