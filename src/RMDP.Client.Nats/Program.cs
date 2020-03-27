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
                    new Subscriber (o.ToNatsOptions ()).Subscribe (nameof (GgdsMsgEvent), OnGgdsHandler);
                });
        }

        static void OnGgdsHandler (object sender, MsgHandlerEventArgs args) {
            var message = Encoding.UTF8.GetString (args.Message.Data);
            var e = (GgdsMsgEvent)JsonSerializer.Deserialize (message, typeof (GgdsMsgEvent));
            Console.WriteLine (JsonSerializer.Serialize(e.Body));
        }
    }

    class CommandOptions {
        public CommandOptions () {
            Url = Defaults.Url;
            Subject = "foo";
        }

        [Option ('l', "url", Default = Defaults.Url, HelpText = "NATS Server Url,'nats://localhost:4222'")]
        public string Url { get; set; }

        [Option ('s', "subject", Default = "foo", HelpText = "订阅主题")]
        public string Subject { get; set; }

        public Options CreateOptions () {
            var opt = ConnectionFactory.GetDefaultOptions ();
            opt.Url = Url;
            return opt;
        }

        public NatsOptions ToNatsOptions () {
            return new NatsOptions () {
                Servers = new [] { this.Url },
                    Exchanges = new string[] { Subject },
                    Username = "guest",
                    Password = "guest"
            };
        }

    }
}