using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace AutotestingOnlineShops.Luma
{
    public class LoginHelper : MainHelper
    {
        public LoginHelper(Manager manager) : base(manager)
        {
        }

        public void SignIn(string login, string password)
        {
            driver.FindElement(By.LinkText("Sign In")).Click();
            driver.FindElement(By.Name("login[username]")).SendKeys(login);
            driver.FindElement(By.Name("login[password]")).SendKeys(password);
            driver.FindElement(By.Name("send")).Click();
        }

        public bool CheckIfLogged(AccountData account)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(30)).Until(e => e.FindElement(By.ClassName("logged-in")));
            if (driver.FindElement(By.CssSelector("span[class='base'][data-ui-id='page-title-wrapper']")).Text != "My Account")
            {
                manager.Navigator.GoToAccountPage();
                new WebDriverWait(driver, TimeSpan.FromSeconds(30)).Until(e => e.FindElement(By.CssSelector("span[class='base'][data-ui-id='page-title-wrapper']")));
            }
            string element = driver.FindElements(By.ClassName("box-content"))[0].FindElement(By.TagName("p")).Text;
            string text = $"{account.FirstName} {account.LastName}\r\n{account.Email}";
            return element == text;
        }
        private void LogOut()
        {
            driver.FindElement(By.CssSelector("button[class='action switch'][data-action='customer-menu-toggle']")).Click();
            driver.FindElement(By.LinkText("Sign Out")).Click();
        }
    }
}
