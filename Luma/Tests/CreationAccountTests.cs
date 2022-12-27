using NUnit.Framework;

namespace AutotestingOnlineShops.Luma
{
    [TestFixture]
    public class CreationAccountTests : TestBase
    {
        [Test]
        public void CreateAccount()
        {
            AccountData newAccount = new AccountData()
            {
                FirstName = GenerateRandomString(5),
                LastName = GenerateRandomString(5),
                Email = GenerateRandomEmail(5),
                Password = GenerateRandomPassword(16)
            };
            app.Account.CreateNewAccount(newAccount);
            app.Credentials.SaveAccountWithoutDefaultAddress(newAccount);
            Assert.IsTrue(app.Login.CheckIfLogged(newAccount));
        }
    }
}
