using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutotestingOnlineShops.Luma
{
    public class AccountHelper : MainHelper
    {
        public AccountHelper(Manager manager) : base(manager)
        {
        }

        public void CreateNewAccount(AccountData newAccount)
        {
            manager.Navigator.GoToCreateAccountPage();
            FillForm(newAccount);
            SubmitCreationNewAccount();
        }

        private void SubmitCreationNewAccount()
        {
            driver.FindElement(By.CssSelector("button[title='Create an Account']")).Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(15)).Until(element => element.FindElement(By.ClassName("logged-in")));
        }

        private void FillForm(AccountData newAccount)
        {
            driver.FindElement(By.Id("firstname")).SendKeys(newAccount.FirstName);
            driver.FindElement(By.Id("lastname")).SendKeys(newAccount.LastName);
            driver.FindElement(By.Id("email_address")).SendKeys(newAccount.Email);
            driver.FindElement(By.Id("password")).SendKeys(newAccount.Password);
            driver.FindElement(By.Id("password-confirmation")).SendKeys(newAccount.Password);
        }
    }
}
