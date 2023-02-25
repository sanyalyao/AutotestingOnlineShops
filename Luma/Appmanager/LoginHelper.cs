using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

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
        public void LogOut()
        {
            Thread.Sleep(10000);
            driver.FindElement(By.ClassName("customer-welcome")).FindElement(By.ClassName("customer-name")).FindElement(By.TagName("button")).Click();
            driver.FindElement(By.ClassName("authorization-link")).FindElement(By.LinkText("Sign Out")).Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(30)).Until(element => element.FindElement(By.Id("ui-id-3")).Text == "What's New");
        }
    }
}
