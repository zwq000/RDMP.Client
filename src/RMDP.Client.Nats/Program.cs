using System;
using System.Text;
using System.Text.Json;
using CommandLine;
using NATS.Client;
using RMDP.Events;

namespace RMDP.Client.Nats
{
    class Program {
        public static void Main (string[] args) {
            Parser.Default.ParseArguments<CommandOptions> (args)
                .WithParsed<CommandOptions> (o => {
                    new Subscriber (o.ToNatsOptions ()).Subscribe (nameof (GgdsMsgEvent), OnGgdsHandler);
                });
        }

        static void OnGgdsHandler (object sender, MsgHandlerEventArgs args) {
            var message = Encoding.UTF8.GetString (args.Message.Data);
            var e = (GgdsMsgEvent)JsonSerializer.Deserialize (message, typeof (GgdsMsgEvent));
            Console.WriteLine (JsonSerializer.Serialize(e.Body));
        }
    }
}