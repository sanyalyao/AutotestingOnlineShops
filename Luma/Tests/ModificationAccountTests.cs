using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using NUnit.Framework;

namespace AutotestingOnlineShops.Luma
{
    [TestFixture]
    public class ModificationAccountTests : TestBase
    {
        [Test]
        public void ModifyAccount()
        {
            AccountData oldAccount = app.Credentials.ReadAccountCredentials();
            AccountData newAccount = new AccountData()
            {
                FirstName = GenerateRandomString(8),
                LastName = GenerateRandomString(8),
                Email = GenerateRandomEmail(7),
                Password = GenerateRandomPassword(8),
            };
            app.Login.SignIn(oldAccount.Email,oldAccount.Password);
            app.Account.ModifyAccount(oldAccount, newAccount);
            app.Credentials.Save(newAccount);
        }
    }
}
