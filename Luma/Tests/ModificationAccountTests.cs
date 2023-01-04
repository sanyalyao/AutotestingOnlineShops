using NUnit.Framework;

namespace AutotestingOnlineShops.Luma
{
    [TestFixture]
    public class ModificationAccountTests : AuthTestBase
    {
        [Test]
        public void ModifyAccount()
        {
            AccountData oldAccount = app.Credentials.ReadAccountCredentials(credentialsCurrentAccount);
            AccountData newAccount = new AccountData()
            {
                FirstName = GenerateRandomString(8),
                LastName = GenerateRandomString(8),
                Email = GenerateRandomEmail(7),
                Password = GenerateRandomPassword(),
            };
            app.Account.ModifyAccount(oldAccount, newAccount);
            app.Credentials.SaveNewCredentialsCurrentAccount(newAccount);
            app.Credentials.SaveAccountWithoutDefaultAddress(newAccount);
            app.Login.SignIn(newAccount);
            string currentAccount = newAccount.FullAccountInfo();
            string newAccountInfo = app.Account.GetFullAccountInfo();
            Assert.AreEqual(currentAccount, newAccountInfo);
        }
    }
}
