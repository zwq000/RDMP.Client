using System;
using CommandLine;

namespace NATSExamples
{
    class Program {
        public static void Main (string[] args) {
            // try {
            //     new Subscriber ().Run (args);
            // } catch (Exception ex) {
            //     Console.Error.WriteLine ("Exception: " + ex.Message);
            //     Console.Error.WriteLine (ex);
            // }
            Parser.Default.ParseArguments<NatsOptions> (args)
                .WithParsed<NatsOptions> (o => {
                    new Subscriber (o).Run ();
                });
        }
    }
}