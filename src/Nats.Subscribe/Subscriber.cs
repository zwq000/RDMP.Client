// Copyright 2015-2018 The NATS Authors
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using NATS.Client;

namespace NATSExamples {
    class Subscriber {
        private int received = 0;
        private NatsOptions options;

        public Subscriber (NatsOptions options) {
            this.options = options;
        }
        public void Run () {
            banner (options);
            Options opts = options.CreateOptions ();

            using (IConnection c = new ConnectionFactory ().CreateConnection (opts)) {
                TimeSpan elapsed;

                if (options.Sync) {
                    elapsed = receiveSyncSubscriber (c, $"{options.Subject}.>");
                } else {
                    elapsed = receiveAsyncSubscriber (c, $"{options.Subject}.>");
                }

                Console.Write ("Received {0} msgs in {1} seconds ", received, elapsed.TotalSeconds);
                Console.WriteLine ("({0} msgs/second).",
                    (int) (received / elapsed.TotalSeconds));
                printStats (c);
            }
        }

        private void printStats (IConnection c) {
            IStatistics s = c.Stats;
            Console.WriteLine ("Statistics:  ");
            Console.WriteLine ("   Incoming Payload Bytes: {0}", s.InBytes);
            Console.WriteLine ("   Incoming Messages: {0}", s.InMsgs);
        }

        private TimeSpan receiveAsyncSubscriber (IConnection c, string subject) {
            Console.WriteLine ("Start AsyncSubscriber for {0}", subject);
            Stopwatch sw = new Stopwatch ();
            Object testLock = new Object ();

            EventHandler<MsgHandlerEventArgs> msgHandler = (sender, args) => {
                if (received == 0)
                    sw.Start ();

                received++;

                if (options.Verbose)
                    Console.WriteLine ("Received: " + args.Message);

                if (received >= options.Count) {
                    sw.Stop ();
                    lock (testLock) {
                        Monitor.Pulse (testLock);
                    }
                }
            };

            using (IAsyncSubscription s = c.SubscribeAsync (subject, msgHandler)) {
                // just wait until we are done.
                lock (testLock) {
                    Monitor.Wait (testLock);
                }
            }

            return sw.Elapsed;
        }

        private TimeSpan receiveSyncSubscriber (IConnection c, string subject) {
            Console.WriteLine ("Start SyncSubscriber for {0}", subject);
            using (ISyncSubscription s = c.SubscribeSync (subject)) {
                Stopwatch sw = new Stopwatch ();

                while (received < options.Count) {
                    if (received == 0)
                        sw.Start ();

                    Msg m = s.NextMessage ();
                    received++;

                    if (options.Verbose)
                        Console.WriteLine ("Received: sub:{0} payload:{1}", m.Subject, Encoding.UTF8.GetString (m.Data));
                }

                sw.Stop ();

                return sw.Elapsed;
            }
        }
        private void banner (NatsOptions options) {
            Console.WriteLine ("Receiving {0} messages on subject {1}",
                options.Count, options.Subject);
            Console.WriteLine ("  Url: {0}", options.Url);
            Console.WriteLine ("  Subject: {0}", options.Subject);
            Console.WriteLine ("  Receiving: {0}",
                options.Sync ? "Synchronously" : "Asynchronously");
        }

    }
}