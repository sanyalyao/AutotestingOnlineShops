using NUnit.Framework;

namespace AutotestingOnlineShops.Luma
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        [Test]
        public void LogIn()
        {
            app.Login.SignIn(defaultAccount.Email, defaultAccount.Password);
            Assert.IsTrue(app.Login.CheckIfLogged(defaultAccount));
        }
    }
}
