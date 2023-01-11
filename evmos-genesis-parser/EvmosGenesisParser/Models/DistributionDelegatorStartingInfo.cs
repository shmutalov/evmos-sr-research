using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvmosGenesisParser.Models
{
    internal class DistributionDelegatorStartingInfo
    {
        [JsonProperty("delegator_address")]
        public string DelegatorAddress { get; set; }

        [JsonProperty("validator_address")]
        public string ValidatorAddress { get; set; }

        [JsonProperty("starting_info")]
        public StartingInfo StartingInfo { get; set; }
    }
}
