using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace AutotestingOnlineShops.Luma
{
    public class NavigationHelper : MainHelper
    {
        private string baseURL;

        public NavigationHelper(Manager manager, string baseURL) : base(manager)
        {
            this.baseURL = baseURL;
        }

        public void OpenHomePage()
        {
            if (driver.Url == baseURL)
            {
                return;
            }
            driver.Navigate().GoToUrl(baseURL);
        }

        public void GoToCreateAccountPage()
        {
            driver.FindElement(By.LinkText("Create an Account")).Click();
        }

        public void GoToAccountPage()
        {
            driver.Navigate().GoToUrl(baseURL + "/customer/account/");
            new WebDriverWait(driver, TimeSpan.FromSeconds(30)).Until(element => element.FindElement(By.CssSelector("span[class='base'][data-ui-id='page-title-wrapper']")).Text == "My Account");
        }

        public void GoToEditAccountPage()
        {
            driver.Navigate().GoToUrl(baseURL + "/customer/account/edit/");
            new WebDriverWait(driver, TimeSpan.FromSeconds(30)).Until(element => element.FindElement(By.CssSelector("span[class='base'][data-ui-id='page-title-wrapper']")).Text == "Edit Account Information");
        }

        public void GoToEditAddressPage()
        {
            driver.Navigate().GoToUrl(baseURL + "/customer/address/new/");
            new WebDriverWait(driver, TimeSpan.FromSeconds(30)).Until(element => element.FindElement(By.CssSelector("span[class='base'][data-ui-id='page-title-wrapper']")).Text == "Add New Address");
        }

        public void GoToAddressBookPage()
        {
            driver.Navigate().GoToUrl(baseURL + "/customer/address");
            new WebDriverWait(driver, TimeSpan.FromSeconds(30)).Until(element => element.FindElement(By.CssSelector("span[class='base'][data-ui-id='page-title-wrapper']")).Text == "Address Book");
        }

        public void GoToManClothesPage()
        {
            driver.Navigate().GoToUrl(baseURL + "/men.html");
            new WebDriverWait(driver, TimeSpan.FromSeconds(30)).Until(element => element.FindElement(By.CssSelector("span[class='base'][data-ui-id='page-title-wrapper']")).Text == "Men");
        }

        public void GoToTopsWomenPage()
        {
            driver.Navigate().GoToUrl(baseURL + "/women/tops-women.html");
            new WebDriverWait(driver, TimeSpan.FromSeconds(30)).Until(element => element.FindElement(By.CssSelector("span[class='base'][data-ui-id='page-title-wrapper']")).Text == "Tops");
        }

        public void GoToShoppingCartPage()
        {
            driver.Navigate().GoToUrl(baseURL + "/checkout/cart/");
            new WebDriverWait(driver, TimeSpan.FromSeconds(30)).Until(element => element.FindElement(By.CssSelector("span[class='base'][data-ui-id='page-title-wrapper']")).Text == "Shopping Cart");
        }

        public void GoToOrderHistoryPage()
        {
            driver.Navigate().GoToUrl(baseURL + "/sales/order/history/");
            new WebDriverWait(driver, TimeSpan.FromSeconds(30)).Until(element => element.FindElement(By.CssSelector("span[class='base'][data-ui-id='page-title-wrapper']")).Text == "My Orders");
        }
    }
}
