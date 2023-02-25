using NUnit.Framework;

namespace AutotestingOnlineShops.Luma
{
    [TestFixture]
    public class LogoutTests : TestBase
    {
        [Test]
        public void LogOut()
        {
            AccountData defaultAccount = app.Credentials.ReadAccountCredentials(credentialsCurrentAccount);
            app.Login.SignIn(defaultAccount);
            Assert.IsTrue(app.Login.CheckIfLogged(defaultAccount));
            app.Login.LogOut();
        }
    }
}
