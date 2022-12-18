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

        internal void ModifyAccount(AccountData oldAccount, AccountData newAccount)
        {
            manager.Navigator.GoToAccountPage();
            manager.Navigator.GoToEditPage();
            driver.FindElement(By.Id("change-email")).Click();
            driver.FindElement(By.Id("change-password")).Click();

            // wait
            new WebDriverWait(driver, TimeSpan.FromSeconds(15)).Until(element => element.FindElement(By.CssSelector("span[data-title='change-email-password']")).Text == "Change Email and Password");

            // firstname
            driver.FindElement(By.Id("firstname")).Clear();
            driver.FindElement(By.Id("firstname")).SendKeys(newAccount.FirstName);

            // lastname
            driver.FindElement(By.Id("lastname")).Clear();
            driver.FindElement(By.Id("lastname")).SendKeys(newAccount.LastName);

            // email
            driver.FindElement(By.Id("email")).Clear();
            driver.FindElement(By.Id("email")).SendKeys(newAccount.Email);

            // current password
            driver.FindElement(By.Id("current-password")).SendKeys(oldAccount.Password);

            // new password
            driver.FindElement(By.Id("password")).SendKeys(newAccount.Password);
            driver.FindElement(By.Id("password-confirmation")).SendKeys(newAccount.Password);

            // submit
            driver.FindElement(By.CssSelector("button[type='submit'][class='action save primary']")).Click();

            // wait
            new WebDriverWait(driver, TimeSpan.FromSeconds(15)).Until(element => element.FindElement(By.CssSelector("span[class='base'][data-ui-id='page-title-wrapper']")));
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
