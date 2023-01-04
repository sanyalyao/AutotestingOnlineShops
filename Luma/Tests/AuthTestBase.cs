using NUnit.Framework;

namespace AutotestingOnlineShops.Luma
{
    public class AuthTestBase : TestBase
    {
        protected AccountData defaultAccount;

        [SetUp]
        public void SetupLogin()
        {
            defaultAccount = app.Credentials.ReadAccountCredentials(credentialsCurrentAccount);
            app.Login.SignIn(defaultAccount);
        }
    }
}
