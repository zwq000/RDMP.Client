using CommandLine;
using NATS.Client;

namespace NATSExamples {
    class NatsOptions {
        public NatsOptions () {
            Verbose = true;
            Url = Defaults.Url;
            Subject = "foo";
            Count = 1000;
        }

        [Option ('v', "verbose", Default = true, Required = false, HelpText = "输出详细信息")]
        public bool Verbose { get; set; }

        [Option ('c', "count", Default = 1000, HelpText = "接收消息数量")]
        public int Count { get; set; }

        [Option ('l', "url", Default = Defaults.Url, HelpText = "NATS Server Url,'nats://localhost:4222'")]
        public string Url { get; set; }

        [Option ('s', "subject", Default = "foo", HelpText = "订阅主题")]
        public string Subject { get; set; }

        [Option ('y', "sync", Default = false, HelpText = "设置同步/异步订阅模式")]
        public bool Sync { get; set; }

        [Option ('u', "user", Default = "guest", HelpText = "登录用户名")]
        public string User { get; set; }

        [Option ('p', "Password", Default = "guest", HelpText = "登录用户密码")]
        public string Password { get; set; }

        public Options CreateOptions () {
            var opt = ConnectionFactory.GetDefaultOptions ();
            opt.Url = Url;
            opt.User = this.User;
            opt.Password = this.Password;
            return opt;
        }
    }
}