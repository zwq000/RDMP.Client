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
using Microsoft.Extensions.Logging;
using NATS.Client;

namespace RMDP.Client {
    class Subscriber {
        private int received = 0;
        private NatsOptions _options;
        private readonly NatsConnection _connection;

        public Subscriber (NatsOptions options) {
            this._options = options;
            this._connection = new NatsConnection (new ConnectionFactory (), options);
        }
        
        public bool Subscribe (string eventName, EventHandler<MsgHandlerEventArgs> handler) {
            if (!_connection.TryConnect ()) return false;

            var conn = _connection.GetConnection ();

            foreach (var exchange in _options.Exchanges) {
                conn.SubscribeAsync ($"{exchange}.{eventName}", _options.Client, handler);
            }
            return true;
        }
    }
}