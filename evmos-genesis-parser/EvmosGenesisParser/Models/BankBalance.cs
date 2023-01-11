using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvmosGenesisParser.Models
{
    internal class BankBalance
    {
        public string Address { get; set; }
        public List<Coin> Coins { get; set; }
    }
}
