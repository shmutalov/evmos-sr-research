using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvmosTransactionsParser.Models
{
    internal class Transaction
    {
        [JsonProperty("txhash")]
        public string TxHash { get; set; }

        public DateTimeOffset Timestamp { get; set; }
        public InternalTx Tx { get; set; }
    }
}
