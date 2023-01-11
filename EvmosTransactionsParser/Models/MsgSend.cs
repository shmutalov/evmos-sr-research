using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvmosTransactionsParser.Models
{
    internal class MsgSend
    {
        [JsonProperty("from_address")]
        public string FromAddress { get; set; }
        
        [JsonProperty("to_address")]
        public string ToAddress { get; set; }

        [JsonProperty("amount")]
        public List<Coin> Amount { get; set; }
    }
}
