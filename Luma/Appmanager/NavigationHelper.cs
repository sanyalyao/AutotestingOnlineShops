﻿using OpenQA.Selenium;
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
        }
    }
}
