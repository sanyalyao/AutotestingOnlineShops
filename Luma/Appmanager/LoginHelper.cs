using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace AutotestingOnlineShops.Luma
{
    public class LoginHelper : MainHelper
    {
        private string baseURL;

        public LoginHelper(Manager manager, string baseURL) : base(manager)
        {
            this.baseURL = baseURL;
        }

        public void SignIn(AccountData account)
        {
            if (CheckIfLogged(account))
            {
                LogOut();
            }
            driver.FindElement(By.LinkText("Sign In")).Click();
            driver.FindElement(By.Name("login[username]")).SendKeys(account.Email);
            driver.FindElement(By.Name("login[password]")).SendKeys(account.Password);
            driver.FindElement(By.Name("send")).Click();
        }

        public bool CheckIfLogged(AccountData account)
        {
            driver.Navigate().GoToUrl(baseURL + "/customer/account/");
            if (driver.FindElement(By.CssSelector("span[class='base'][data-ui-id='page-title-wrapper']")).Text == "Customer Login")
            {
                return false;
            }
            if (driver.FindElement(By.CssSelector("span[class='base'][data-ui-id='page-title-wrapper']")).Text == "My Account")
            {
                string element = driver.FindElements(By.ClassName("box-content"))[0].FindElement(By.TagName("p")).Text;
                string text = $"{account.FirstName} {account.LastName}\r\n{account.Email}";
                return element == text;
            }
            else
            {
                return false;
            }
        }
        private void LogOut()
        {
            driver.FindElement(By.CssSelector("button[class='action switch'][data-action='customer-menu-toggle']")).Click();
            driver.FindElement(By.CssSelector("div[class='customer-menu'][data-target='dropdown']")).FindElement(By.LinkText("Sign Out")).Click();
        }
    }
}
