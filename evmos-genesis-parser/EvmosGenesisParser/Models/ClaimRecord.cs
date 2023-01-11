using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvmosGenesisParser.Models
{
    internal class ClaimRecord
    {
        public string Address { get; set; }

        [JsonProperty("initial_claimable_amount")]
        public string InitialClaimableAmount { get; set; }
    }
}
