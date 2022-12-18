using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace AutotestingOnlineShops.Luma
{
    public class CredentialsHelper : MainHelper
    {
        public CredentialsHelper(Manager manager) : base(manager)
        {
        }

        public void Save(AccountData account)
        {
            StreamWriter writer = new StreamWriter("account_credentials.json");
            writer.Write(JsonConvert.SerializeObject(account, Formatting.Indented));
            writer.Close();
        }
        public AccountData ReadAccountCredentials()
        {
            AccountData account = JsonConvert.DeserializeObject<AccountData>(File.ReadAllText("account_credentials.json"));
            return account;
        }
    }
}
