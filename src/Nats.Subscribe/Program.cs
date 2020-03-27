using System;
using CommandLine;

namespace NATSExamples {
    class Program {
        public static void Main (string[] args) {
            Parser.Default.ParseArguments<NatsOptions> (args)
                .WithParsed<NatsOptions> (o => {
                    new Subscriber (o).Run ();
                });
        }
    }
}