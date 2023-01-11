using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvmosTransactionsParser.Models
{
    internal class InternalTxBody
    {
        public List<MsgSend> Messages { get; set; }
        public string Memo { get; set; }
    }
}
