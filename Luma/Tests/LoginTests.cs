using NUnit.Framework;

namespace AutotestingOnlineShops.Luma
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        [Test]
        public void LogIn()
        {
            AccountData defaultAccount = app.Credentials.ReadAccountCredentials();
            app.Login.SignIn(defaultAccount.Email, defaultAccount.Password);
            Assert.IsTrue(app.Login.CheckIfLogged(defaultAccount));
        }
    }
}
