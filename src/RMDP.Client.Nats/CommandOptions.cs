using CommandLine;
using NATS.Client;

namespace RMDP.Client.Nats
{
    class CommandOptions {
        public CommandOptions () {
            Url = Defaults.Url;
            Subject = "rmdp";
        }

        [Option ('l', "url", Default = Defaults.Url, HelpText = "NATS Server Url,'nats://localhost:4222'")]
        public string Url { get; set; }

        [Option ('s', "subject", Default = "rmdp", HelpText = "订阅主题")]
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