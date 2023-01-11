using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvmosTransactionsParser.Models
{
    internal class Coin
    {
        public string Denom { get; set; }
        public string Amount { get; set; }
    }
}
