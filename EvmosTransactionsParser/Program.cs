using EvmosTransactionsParser.Models;
using Newtonsoft.Json;
using System.Numerics;

var transactions = JsonConvert.DeserializeObject<Result>(await File.ReadAllTextAsync("D:\\Projects\\mine\\EvmosTransactionsParser\\evmos-sr-transfers.json"));

using var file = new StreamWriter("D:\\Projects\\mine\\EvmosTransactionsParser\\evmos-sr-transfers.csv");
await file.WriteLineAsync("From,To,Amount,Denom,Tx,Date");
foreach (var transaction in transactions.Txs)
{
    foreach (var msg in transaction.Tx.Body.Messages)
    {
        foreach (var coin in msg.Amount)
        {
            var amount = BigInteger.Divide(BigInteger.Parse(coin.Amount), new BigInteger(1_000_000_000_000_000_000m));
            await file.WriteLineAsync($"{msg.FromAddress},{msg.ToAddress},{amount},{coin.Denom},{transaction.TxHash},{transaction.Timestamp:s}");
        }
    }
}