using System.IO;
using Newtonsoft.Json;

namespace AutotestingOnlineShops.Luma
{
    public class CredentialsHelper : MainHelper
    {
        public CredentialsHelper(Manager manager) : base(manager)
        {
        }

        public void SaveNewCredentialsCurrentAccount(AccountData account)
        {
            StreamWriter writer = new StreamWriter("account_credentials.json");
            writer.Write(JsonConvert.SerializeObject(account, Formatting.Indented));
            writer.Close();
        }

        public void SaveAccountWithoutDefaultAddress(AccountData account)
        {
            StreamWriter writer = new StreamWriter("account_without_default_address.json");
            writer.Write(JsonConvert.SerializeObject(account, Formatting.Indented));
            writer.Close();
        }

        public AccountData ReadAccountCredentials(string file)
        {
            AccountData account = JsonConvert.DeserializeObject<AccountData>(File.ReadAllText(file));
            return account;
        }

        internal void SaveAccountWithDefaultAddress(AccountData accountWithoutAddress)
        {
            StreamWriter writer = new StreamWriter("account_with_default_address.json");
            writer.Write(JsonConvert.SerializeObject(accountWithoutAddress, Formatting.Indented));
            writer.Close();

        }
    }
}
