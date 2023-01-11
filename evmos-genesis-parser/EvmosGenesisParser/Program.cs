using EvmosGenesisParser.Models;
using Newtonsoft.Json;
using System.Data.SQLite;
using Dapper;
using System.Data;
using System.Security.Principal;
using System.Numerics;

Console.WriteLine("Creating the SQLite database...");

using var connection = new SQLiteConnection("DataSource=D:\\Projects\\mine\\evmos-genesis-parser\\EvmosGenesisParser\\genesis.sqlite3;New=True");
await connection.OpenAsync();

await CreateTablesAsync(connection);

var pragmas = @"PRAGMA synchronous=OFF;
    PRAGMA count_changes=OFF;
    PRAGMA journal_mode=MEMORY;
    PRAGMA temp_store=MEMORY;
    PRAGMA encoding=""UTF-8""";
await connection.ExecuteAsync(pragmas);

Console.WriteLine("Reading the Genesis...");

using var f = File.OpenText("D:\\Projects\\mine\\evmos-genesis-parser\\EvmosGenesisParser\\genesis.json");
using var reader = new JsonTextReader(f);

while (reader.Read())
{
    if (reader.TokenType == JsonToken.StartArray && reader.Path == "app_state.auth.accounts")
    {
        await ParseAuthAccountsAsync(reader, connection);
    }

    if (reader.TokenType == JsonToken.StartObject && reader.Path == "app_state.auth.params")
    {
        ParseAuthParams(reader);
    }

    if (reader.TokenType == JsonToken.StartObject && reader.Path == "app_state.authz")
    {
        ParseAuthz(reader);
    }

    if (reader.TokenType == JsonToken.StartArray && reader.Path == "app_state.bank.balances")
    {
        await ParseBankBalancesAsync(reader, connection);
    }

    if (reader.TokenType == JsonToken.StartArray && reader.Path == "app_state.bank.denom_metadata")
    {
        ParseBankDenomMetadata(reader);
    }

    if (reader.TokenType == JsonToken.StartObject && reader.Path == "app_state.bank.params")
    {
        ParseBankParams(reader);
    }

    if (reader.TokenType == JsonToken.StartArray && reader.Path == "app_state.bank.supply")
    {
        await ParseBankSupplyAsync(reader, connection);
    }

    if (reader.TokenType == JsonToken.StartObject && reader.Path == "app_state.capability")
    {
        ParseCapability(reader);
    }

    if (reader.TokenType == JsonToken.StartArray && reader.Path == "app_state.claims.claims_records")
    {
        await ParseClaimRecordsAsync(reader, connection);
    }

    if (reader.TokenType == JsonToken.StartObject && reader.Path == "app_state.claims.params")
    {
        ParseClaimParams(reader);
    }

    if (reader.TokenType == JsonToken.StartObject && reader.Path == "app_state.crisis")
    {
        ParseCrisis(reader);
    }

    if (reader.TokenType == JsonToken.StartArray && reader.Path == "app_state.distribution.delegator_starting_infos")
    {
        ParseDistributionDelegatorStartingInfos(reader);
    }

    if (reader.TokenType == JsonToken.StartArray && reader.Path == "app_state.distribution.delegator_withdraw_infos")
    {
        ParseDistributionDelegatorWithdrawInfos(reader);
    }

    if (reader.TokenType == JsonToken.StartObject && reader.Path == "app_state.distribution.fee_pool")
    {
        ParseDistributionFeePool(reader);
    }

    if (reader.TokenType == JsonToken.StartArray && reader.Path == "app_state.distribution.outstanding_rewards")
    {
        ParseDistributionOutstandingRewards(reader);
    }

    if (reader.TokenType == JsonToken.StartObject && reader.Path == "app_state.distribution.params")
    {
        ParseDistributionParams(reader);
    }

    if (reader.Path == "app_state.distribution.previous_proposer")
    {
        ParseDistributionPreviousProposer(reader);
    }

    if (reader.TokenType == JsonToken.StartArray && reader.Path == "app_state.distribution.validator_accumulated_commissions")
    {
        ParseDistributionValidatorAccumulatedCommissions(reader);
    }

    if (reader.TokenType == JsonToken.StartArray && reader.Path == "app_state.distribution.validator_current_rewards")
    {
        ParseDistributionValidatorCurrentRewards(reader);
    }

    if (reader.TokenType == JsonToken.StartArray && reader.Path == "app_state.distribution.validator_historical_rewards")
    {
        ParseDistributionValidatorHistoricalRewards(reader);
    }

    if (reader.TokenType == JsonToken.StartArray && reader.Path == "app_state.distribution.validator_slash_events")
    {
        ParseDistributionValidatorSlashEvents(reader);
    }

    if (reader.TokenType == JsonToken.StartObject && reader.Path == "app_state.epochs")
    {
        ParseEpochs(reader);
    }

    if (reader.TokenType == JsonToken.StartObject && reader.Path == "app_state.erc20")
    {
        ParseErc20(reader);
    }

    if (reader.TokenType == JsonToken.StartObject && reader.Path == "app_state.evidence")
    {
        ParseEvidence(reader);
    }

    if (reader.TokenType == JsonToken.StartArray && reader.Path == "app_state.evm.accounts")
    {
        ParseEvmAccounts(reader);
    }

    if (reader.TokenType == JsonToken.StartObject && reader.Path == "app_state.evm.params")
    {
        ParseEvmParams(reader);
    }

    if (reader.TokenType == JsonToken.StartObject && reader.Path == "app_state.feegrant")
    {
        ParseFeeGrant(reader);
    }

    if (reader.TokenType == JsonToken.StartObject && reader.Path == "app_state.feemarket")
    {
        ParseFeeMarket(reader);
    }

    if (reader.TokenType == JsonToken.StartObject && reader.Path == "app_state.genutil")
    {
        ParseGenUtil(reader);
    }

    if (reader.TokenType == JsonToken.StartObject && reader.Path == "app_state.gov")
    {
        ParseGov(reader);
    }

    if (reader.TokenType == JsonToken.StartObject && reader.Path == "app_state.ibc")
    {
        ParseIbc(reader);
    }

    if (reader.TokenType == JsonToken.StartObject && reader.Path == "app_state.incentives")
    {
        ParseIncentives(reader);
    }

    if (reader.TokenType == JsonToken.StartObject && reader.Path == "app_state.inflation")
    {
        ParseInflation(reader);
    }

    if (reader.TokenType == JsonToken.StartObject && reader.Path == "app_state.params")
    {
        ParseParams(reader);
    }

    if (reader.TokenType == JsonToken.StartObject && reader.Path == "app_state.slashing")
    {
        ParseSlashing(reader);
    }

    if (reader.TokenType == JsonToken.StartObject && reader.Path == "app_state.staking")
    {
        ParseStaking(reader);
    }

    if (reader.TokenType == JsonToken.StartObject && reader.Path == "app_state.transfer")
    {
        ParseTransfer(reader);
    }

    if (reader.TokenType == JsonToken.StartObject && reader.Path == "app_state.upgrade")
    {
        ParseUpgrade(reader);
    }

    if (reader.TokenType == JsonToken.StartObject && reader.Path == "app_state.vesting")
    {
        ParseVesting(reader);
    }

    if (reader.TokenType == JsonToken.StartObject && reader.Path == "app_state.recovery")
    {
        ParseRecovery(reader);
    }

    if (reader.TokenType == JsonToken.StartObject && reader.Path == "chain_id")
    {
        //ParseUpgrade(reader);
        continue;
    }

    if (reader.TokenType == JsonToken.StartObject && reader.Path == "consensus_params")
    {
        ParseConsensusParams(reader);
    }

    if (reader.TokenType == JsonToken.StartObject && reader.Path == "genesis_time")
    {
        //ParseConsensusParams(reader);
        continue;
    }

    if (reader.TokenType == JsonToken.StartObject && reader.Path == "initial_height")
    {
        //ParseConsensusParams(reader);
        continue;
    }

    if (reader.TokenType == JsonToken.StartArray && reader.Path == "validators")
    {
        ParseValidators(reader);
    }
}

Console.WriteLine("Done");

async Task ParseAuthAccountsAsync(JsonTextReader reader, IDbConnection connection)
{
    Console.WriteLine("Parsing accounts...");
    var count = 0;
    do
    {
        if (reader.TokenType == JsonToken.EndArray && reader.Path == "app_state.auth.accounts") break;
        if (reader.TokenType == JsonToken.StartObject && reader.Path.EndsWith("base_account"))
        {
            if (TryParseBaseAccount(reader, out var account))
            {
                await connection.ExecuteAsync("INSERT INTO auth_accounts VALUES (@Address)", account);
                count++;
            }
        }
    } while (reader.Read());

    Console.WriteLine("Accounts parsed {0}", count);
}

bool TryParseBaseAccount(JsonTextReader reader, out BaseAccount account)
{
    account = new JsonSerializer().Deserialize<BaseAccount>(reader);
    return account != null;
}

void ParseAuthParams(JsonTextReader reader)
{
    Console.WriteLine("Parsing auth params...");
    do
    {
        if (reader.TokenType == JsonToken.EndObject && reader.Path == "app_state.auth.params") break;
    } while (reader.Read());

    Console.WriteLine("Auth params parsing skipped");
}

void ParseAuthz(JsonTextReader reader)
{
    Console.WriteLine("Parsing authz...");
    do
    {
        if (reader.TokenType == JsonToken.EndObject && reader.Path == "app_state.authz") break;
    } while (reader.Read());

    Console.WriteLine("Authz parsing skipped");
}

async Task ParseBankBalancesAsync(JsonTextReader reader, IDbConnection connection)
{
    Console.WriteLine("Parsing balances...");
    var count = 0;
    do
    {
        if (reader.TokenType == JsonToken.EndArray && reader.Path == "app_state.bank.balances") break;
        if (reader.TokenType == JsonToken.StartObject)
        {
            if (TryParseBankBalance(reader, out var balance))
            {
                foreach (var coin in balance.Coins)
                {
                    var amount = BigInteger.Parse(coin.Amount);
                    amount = BigInteger.Divide(amount, new BigInteger(1_000_000_000_000_000_000m));
                    await connection.ExecuteAsync("INSERT INTO bank_balances (address, denom, amount) VALUES (@Address, @Denom, @Amount)", new
                    {
                        balance.Address,
                        coin.Denom,
                        Amount = ((decimal)amount)
                    });
                }
               
                count++;
            }

        }
    } while (reader.Read());

    Console.WriteLine("Balances parsed {0}", count);
}

bool TryParseBankBalance(JsonTextReader reader, out BankBalance balance)
{
    balance = new JsonSerializer().Deserialize<BankBalance>(reader);
    return balance != null;
}

void ParseBankDenomMetadata(JsonTextReader reader)
{
    Console.WriteLine("Parsing bank denom metadata...");
    do
    {
        if (reader.TokenType == JsonToken.EndArray && reader.Path == "app_state.bank.denom_metadata") break;
    } while (reader.Read());

    Console.WriteLine("Bank denom metadata parsing skipped");
}

void ParseBankParams(JsonTextReader reader)
{
    Console.WriteLine("Parsing bank params...");
    do
    {
        if (reader.TokenType == JsonToken.EndObject && reader.Path == "app_state.bank.params") break;
    } while (reader.Read());

    Console.WriteLine("Bank params parsing skipped");
}

async Task ParseBankSupplyAsync(JsonTextReader reader, IDbConnection connection)
{
    Console.WriteLine("Parsing bank supply...");
    var count = 0;
    do
    {
        if (reader.TokenType == JsonToken.EndArray && reader.Path == "app_state.bank.supply") break;
        if (reader.TokenType == JsonToken.StartObject)
        {
            if (TryParseBankSupply(reader, out var supply))
            {
                var amount = BigInteger.Parse(supply.Amount);
                amount = BigInteger.Divide(amount, new BigInteger(1_000_000_000_000_000_000m));
                await connection.ExecuteAsync("INSERT INTO bank_supply (denom, amount) VALUES (@Denom, @Amount)", new
                {
                    supply.Denom,
                    Amount = ((decimal)amount)
                });

                count++;
            }

        }
    } while (reader.Read());

    Console.WriteLine("Bank supply parsed {0}", count);
}

bool TryParseBankSupply(JsonTextReader reader, out Coin coin)
{
    coin = new JsonSerializer().Deserialize<Coin>(reader);
    return coin != null;
}

void ParseCapability(JsonTextReader reader)
{
    Console.WriteLine("Parsing capabilities...");
    do
    {
        if (reader.TokenType == JsonToken.EndObject && reader.Path == "app_state.capability") break;
    } while (reader.Read());

    Console.WriteLine("Capabilities parsing skipped");
}

async Task ParseClaimRecordsAsync(JsonTextReader reader, IDbConnection connection)
{
    Console.WriteLine("Parsing claim records...");
    var count = 0;
    do
    {
        if (reader.TokenType == JsonToken.EndArray && reader.Path == "app_state.claims.claims_records") break;
        if (reader.TokenType == JsonToken.StartObject)
        {
            if (TryParseClaimRecord(reader, out var claim))
            {
                var amount = BigInteger.Parse(claim.InitialClaimableAmount);
                amount = BigInteger.Divide(amount, new BigInteger(1_000_000_000_000_000_000m));
                await connection.ExecuteAsync("INSERT INTO claim_records (address, amount) VALUES (@Address, @Amount)", new
                {
                    claim.Address,
                    Amount = ((decimal)amount)
                });

                count++;
            }

        }
    } while (reader.Read());

    Console.WriteLine("Claim records parsed {0}", count);
}

bool TryParseClaimRecord(JsonTextReader reader, out ClaimRecord claim)
{
    claim = new JsonSerializer().Deserialize<ClaimRecord>(reader);
    return claim != null;
}

void ParseClaimParams(JsonTextReader reader)
{
    Console.WriteLine("Parsing claim params...");
    do
    {
        if (reader.TokenType == JsonToken.EndObject && reader.Path == "app_state.claims.params") break;
    } while (reader.Read());

    Console.WriteLine("Claim params parsing skipped");
}

void ParseCrisis(JsonTextReader reader)
{
    Console.WriteLine("Parsing crisis...");
    do
    {
        if (reader.TokenType == JsonToken.EndObject && reader.Path == "app_state.crisis") break;
    } while (reader.Read());

    Console.WriteLine("Crisis parsing skipped");
}

void ParseDistributionDelegatorStartingInfos(JsonTextReader reader)
{
    Console.WriteLine("Parsing distribution delegator starting infos...");
    var count = 0;
    do
    {
        if (reader.TokenType == JsonToken.EndArray && reader.Path == "app_state.distribution.delegator_starting_infos") break;
        if (reader.TokenType == JsonToken.StartObject)
        {
            if (TryParseDistributionDelegatorStartingInfo(reader, out var delegation))
            {
                count++;
            }

        }
    } while (reader.Read());

    Console.WriteLine("Distribution delegator starting infos parsed {0}", count);
}

bool TryParseDistributionDelegatorStartingInfo(JsonTextReader reader, out DistributionDelegatorStartingInfo delegation)
{
    delegation = new JsonSerializer().Deserialize<DistributionDelegatorStartingInfo>(reader);
    return delegation != null;
}

void ParseDistributionDelegatorWithdrawInfos(JsonTextReader reader)
{
    Console.WriteLine("Parsing distribution delegator withdraw infos...");
    do
    {
        if (reader.TokenType == JsonToken.EndArray && reader.Path == "app_state.distribution.delegator_withdraw_infos") break;
    } while (reader.Read());

    Console.WriteLine("Distribution delegator withdraw infos parsing skipped");
}

void ParseDistributionFeePool(JsonTextReader reader)
{
    Console.WriteLine("Parsing distribution fee pool...");
    do
    {
        if (reader.TokenType == JsonToken.EndObject && reader.Path == "app_state.distribution.fee_pool") break;
    } while (reader.Read());

    Console.WriteLine("Distribution fee pool parsing skipped");
}

void ParseDistributionOutstandingRewards(JsonTextReader reader)
{
    Console.WriteLine("Parsing distribution outstanding rewards...");
    do
    {
        if (reader.TokenType == JsonToken.EndArray && reader.Path == "app_state.distribution.outstanding_rewards") break;
    } while (reader.Read());

    Console.WriteLine("Distribution outstanding rewards parsing skipped");
}

void ParseDistributionParams(JsonTextReader reader)
{
    Console.WriteLine("Parsing distribution params...");
    do
    {
        if (reader.TokenType == JsonToken.EndObject && reader.Path == "app_state.distribution.params") break;
    } while (reader.Read());

    Console.WriteLine("Distribution params parsing skipped");
}

void ParseDistributionPreviousProposer(JsonTextReader reader)
{
    Console.WriteLine("Parsing distribution previous proposer...");

    reader.Read();

    Console.WriteLine("Distribution previous proposer parsing skipped");
}

void ParseDistributionValidatorAccumulatedCommissions(JsonTextReader reader)
{
    Console.WriteLine("Parsing distribution validator accumulated commissions...");
    do
    {
        if (reader.TokenType == JsonToken.EndArray && reader.Path == "app_state.distribution.validator_accumulated_commissions") break;
    } while (reader.Read());

    Console.WriteLine("Distribution validator accumulated commissions parsing skipped");
}

void ParseDistributionValidatorCurrentRewards(JsonTextReader reader)
{
    Console.WriteLine("Parsing distribution validator current rewards...");
    do
    {
        if (reader.TokenType == JsonToken.EndArray && reader.Path == "app_state.distribution.validator_current_rewards") break;
    } while (reader.Read());

    Console.WriteLine("Distribution validator current rewards parsing skipped");
}

void ParseDistributionValidatorHistoricalRewards(JsonTextReader reader)
{
    Console.WriteLine("Parsing validator historical rewards...");
    do
    {
        if (reader.TokenType == JsonToken.EndArray && reader.Path == "app_state.distribution.validator_historical_rewards") break;
    } while (reader.Read());

    Console.WriteLine("Validator historical rewards parsing skipped");
}

void ParseDistributionValidatorSlashEvents(JsonTextReader reader)
{
    Console.WriteLine("Parsing distribution validator slash events...");
    do
    {
        if (reader.TokenType == JsonToken.EndArray && reader.Path == "app_state.distribution.validator_slash_events") break;
    } while (reader.Read());

    Console.WriteLine("Distribution validator slash events parsing skipped");
}

void ParseEpochs(JsonTextReader reader)
{
    Console.WriteLine("Parsing epochs...");
    do
    {
        if (reader.TokenType == JsonToken.EndObject && reader.Path == "app_state.epochs") break;
    } while (reader.Read());

    Console.WriteLine("Epochs parsing skipped");
}

void ParseErc20(JsonTextReader reader)
{
    Console.WriteLine("Parsing ERC20...");
    do
    {
        if (reader.TokenType == JsonToken.EndObject && reader.Path == "app_state.erc20") break;
    } while (reader.Read());

    Console.WriteLine("ERC20 parsing skipped");
}

void ParseEvidence(JsonTextReader reader)
{
    Console.WriteLine("Parsing evidence...");
    do
    {
        if (reader.TokenType == JsonToken.EndObject && reader.Path == "app_state.evidence") break;
    } while (reader.Read());

    Console.WriteLine("Evidence parsing skipped");
}

void ParseEvmAccounts(JsonTextReader reader)
{
    Console.WriteLine("Parsing EVM accounts...");
    do
    {
        if (reader.TokenType == JsonToken.EndArray && reader.Path == "app_state.evm.accounts") break;
    } while (reader.Read());

    Console.WriteLine("EVM accounts parsing skipped");
}

void ParseEvmParams(JsonTextReader reader)
{
    Console.WriteLine("Parsing EVM params...");
    do
    {
        if (reader.TokenType == JsonToken.EndObject && reader.Path == "app_state.evm.params") break;
    } while (reader.Read());

    Console.WriteLine("EVM params parsing skipped");
}

void ParseFeeGrant(JsonTextReader reader)
{
    Console.WriteLine("Parsing fee grant...");
    do
    {
        if (reader.TokenType == JsonToken.EndObject && reader.Path == "app_state.feegrant") break;
    } while (reader.Read());

    Console.WriteLine("Fee grant parsing skipped");
}

void ParseFeeMarket(JsonTextReader reader)
{
    Console.WriteLine("Parsing fee market...");
    do
    {
        if (reader.TokenType == JsonToken.EndObject && reader.Path == "app_state.feemarket") break;
    } while (reader.Read());

    Console.WriteLine("Fee market parsing skipped");
}

void ParseGenUtil(JsonTextReader reader)
{
    Console.WriteLine("Parsing genutil...");
    do
    {
        if (reader.TokenType == JsonToken.EndObject && reader.Path == "app_state.genutil") break;
    } while (reader.Read());

    Console.WriteLine("Genutil parsing skipped");
}

void ParseGov(JsonTextReader reader)
{
    Console.WriteLine("Parsing gov...");
    do
    {
        if (reader.TokenType == JsonToken.EndObject && reader.Path == "app_state.gov") break;
    } while (reader.Read());

    Console.WriteLine("Gov parsing skipped");
}

void ParseIbc(JsonTextReader reader)
{
    Console.WriteLine("Parsing IBC...");
    do
    {
        if (reader.TokenType == JsonToken.EndObject && reader.Path == "app_state.ibc") break;
    } while (reader.Read());

    Console.WriteLine("IBC parsing skipped");
}

void ParseIncentives(JsonTextReader reader)
{
    Console.WriteLine("Parsing incentives...");
    do
    {
        if (reader.TokenType == JsonToken.EndObject && reader.Path == "app_state.incentives") break;
    } while (reader.Read());

    Console.WriteLine("Incentives parsing skipped");
}

void ParseInflation(JsonTextReader reader)
{
    Console.WriteLine("Parsing inflation...");
    do
    {
        if (reader.TokenType == JsonToken.EndObject && reader.Path == "app_state.inflation") break;
    } while (reader.Read());

    Console.WriteLine("Inflation parsing skipped");
}

void ParseParams(JsonTextReader reader)
{
    Console.WriteLine("Parsing params...");
    do
    {
        if (reader.TokenType == JsonToken.EndObject && reader.Path == "app_state.params") break;
    } while (reader.Read());

    Console.WriteLine("Params parsing skipped");
}

void ParseSlashing(JsonTextReader reader)
{
    Console.WriteLine("Parsing slashing...");
    do
    {
        if (reader.TokenType == JsonToken.EndObject && reader.Path == "app_state.slashing") break;
    } while (reader.Read());

    Console.WriteLine("Slashing parsing skipped");
}

void ParseStaking(JsonTextReader reader)
{
    Console.WriteLine("Parsing staking...");
    do
    {
        if (reader.TokenType == JsonToken.EndObject && reader.Path == "app_state.staking") break;
    } while (reader.Read());

    Console.WriteLine("Staking parsing skipped");
}

void ParseTransfer(JsonTextReader reader)
{
    Console.WriteLine("Parsing transfer...");
    do
    {
        if (reader.TokenType == JsonToken.EndObject && reader.Path == "app_state.transfer") break;
    } while (reader.Read());

    Console.WriteLine("Transfer parsing skipped");
}

void ParseUpgrade(JsonTextReader reader)
{
    Console.WriteLine("Parsing upgrade...");
    do
    {
        if (reader.TokenType == JsonToken.EndObject && reader.Path == "app_state.upgrade") break;
    } while (reader.Read());

    Console.WriteLine("Upgrade parsing skipped");
}

void ParseVesting(JsonTextReader reader)
{
    Console.WriteLine("Parsing vesting...");
    do
    {
        if (reader.TokenType == JsonToken.EndObject && reader.Path == "app_state.vesting") break;
    } while (reader.Read());

    Console.WriteLine("Vesting parsing skipped");
}

void ParseRecovery(JsonTextReader reader)
{
    Console.WriteLine("Parsing recovery...");
    do
    {
        if (reader.TokenType == JsonToken.EndObject && reader.Path == "app_state.recovery") break;
    } while (reader.Read());

    Console.WriteLine("Recovery parsing skipped");
}

void ParseConsensusParams(JsonTextReader reader)
{
    Console.WriteLine("Parsing consensus_params...");
    do
    {
        if (reader.TokenType == JsonToken.EndObject && reader.Path == "consensus_params") break;
    } while (reader.Read());

    Console.WriteLine("Consensus params parsing skipped");
}

void ParseValidators(JsonTextReader reader)
{
    Console.WriteLine("Parsing validators...");
    do
    {
        if (reader.TokenType == JsonToken.EndArray && reader.Path == "validators") break;
    } while (reader.Read());

    Console.WriteLine("Validators params parsing skipped");
}

async Task CreateTablesAsync(IDbConnection connection)
{
    // auth accounts
    var sql = @"CREATE TABLE IF NOT EXISTS auth_accounts (address TEXT)";
    Console.WriteLine(sql);
    _ = await connection.ExecuteAsync(sql);

    sql = @"CREATE TABLE IF NOT EXISTS bank_balances (address TEXT, denom TEXT, amount INTEGER)";
    Console.WriteLine(sql);
    _ = await connection.ExecuteAsync(sql);

    sql = @"CREATE TABLE IF NOT EXISTS bank_supply (denom TEXT, amount INTEGER)";
    Console.WriteLine(sql);
    _ = await connection.ExecuteAsync(sql);

    sql = @"CREATE TABLE IF NOT EXISTS claim_records (address TEXT, amount INTEGER)";
    Console.WriteLine(sql);
    _ = await connection.ExecuteAsync(sql);
}