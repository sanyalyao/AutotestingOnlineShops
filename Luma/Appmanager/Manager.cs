using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Firefox;
using System.Threading;

namespace AutotestingOnlineShops.Luma
{
    public class Manager
    {
        protected IWebDriver driver;
        protected string baseURL;
        protected NavigationHelper navigationHelper;
        protected LoginHelper loginHelper;
        protected AccountHelper accountHelper;
        protected CredentialsHelper credentialsHelper;
        private static ThreadLocal<Manager> app = new ThreadLocal<Manager>();


        private Manager()
        {
            driver = new FirefoxDriver();
            baseURL = "https://magento.softwaretestingboard.com/";
            navigationHelper = new NavigationHelper(this, baseURL);
            loginHelper = new LoginHelper(this);
            accountHelper = new AccountHelper(this);
            credentialsHelper = new CredentialsHelper(this);
            // helpers below
        }

        ~Manager()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        public static Manager GetInstance()
        {
            if (!app.IsValueCreated)
            {
                Manager newInstance = new Manager();
                newInstance.Navigator.OpenHomePage();
                app.Value = newInstance;
            }
            return app.Value;
        }

        public IWebDriver Driver
        {
            get
            {
                return driver;
            }
        }

        public NavigationHelper Navigator
        {
            get
            {
                return navigationHelper;
            }
        }

        public LoginHelper Login
        {
            get
            {
                return loginHelper;
            }
        }

        public AccountHelper Account
        {
            get
            {
                return accountHelper;
            }
        }

        public CredentialsHelper Credentials
        {
            get
            {
                return credentialsHelper;
            }
        }
    }
}
