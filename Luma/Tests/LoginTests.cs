using NUnit.Framework;

namespace AutotestingOnlineShops.Luma
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        [Test]
        public void LogIn()
        {
            AccountData defaultAccount = app.Credentials.ReadAccountCredentials(credentialsCurrentAccount);
            app.Login.SignIn(defaultAccount);
            Assert.IsTrue(app.Login.CheckIfLogged(defaultAccount));
        }
    }
}
