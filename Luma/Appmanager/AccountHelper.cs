using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Linq;
using System.Collections.Generic;
using OpenQA.Selenium.Interactions;

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

        public void SubmitCreationNewAccount()
        {
            driver.FindElement(By.CssSelector("button[title='Create an Account']")).Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(15)).Until(element => element.FindElement(By.ClassName("logged-in")));
        }

        public void AddDefaultAddress(AddressData address)
        {
            manager.Navigator.GoToEditAddressPage();

            // company name
            driver.FindElement(By.Id("company")).Clear();
            driver.FindElement(By.Id("company")).SendKeys(address.CompanyName);

            // phone number
            driver.FindElement(By.Id("telephone")).Clear();
            driver.FindElement(By.Id("telephone")).SendKeys(address.PhoneNumber);

            // street address
            driver.FindElement(By.Id("street_1")).Clear();
            driver.FindElement(By.Id("street_1")).SendKeys(address.StreetAddress);

            // city
            driver.FindElement(By.Id("city")).Clear();
            driver.FindElement(By.Id("city")).SendKeys(address.City);

            // wait
            new WebDriverWait(driver, TimeSpan.FromSeconds(60)).Until(element => element.FindElement(By.CssSelector("select[id='region_id'][name='region_id']")).Displayed);

            // state
            var number = driver.FindElement(By.Id("region_id")).FindElements(By.TagName("option")).Where(element => element.Text.ToLower() == address.State.ToLower()).First().GetAttribute("value");
            driver.FindElement(By.Id("region_id")).Click();
            new SelectElement(driver.FindElement(By.Id("region_id"))).SelectByText(address.State);
            driver.FindElement(By.XPath($"//option[@value='{number}']")).Click();

            // zip code
            driver.FindElement(By.Id("zip")).Clear();
            driver.FindElement(By.Id("zip")).SendKeys(address.Zip);

            // country
            //driver.FindElement(By.Id("country")).FindElements(By.TagName("option")).Where(element => element.Text == address.Country).First().Click();

            driver.FindElement(By.CssSelector("button[data-action='save-address']")).Click();
        }

        public string GetFullAccountInfo()
        {
            manager.Navigator.GoToAccountPage();
            return driver.FindElement(By.CssSelector("div[class='box box-information']")).FindElement(By.TagName("p")).Text;
        }

        public void ModifyAccount(AccountData oldAccount, AccountData newAccount)
        {
            manager.Navigator.GoToEditAccountPage();
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
            new WebDriverWait(driver, TimeSpan.FromSeconds(15)).Until(element => element.FindElement(By.CssSelector("div[class='message-success success message']")).Text == "You saved the account information.");
        }

        public void FillForm(AccountData newAccount)
        {
            driver.FindElement(By.Id("firstname")).SendKeys(newAccount.FirstName);
            driver.FindElement(By.Id("lastname")).SendKeys(newAccount.LastName);
            driver.FindElement(By.Id("email_address")).SendKeys(newAccount.Email);
            driver.FindElement(By.Id("password")).SendKeys(newAccount.Password);
            driver.FindElement(By.Id("password-confirmation")).SendKeys(newAccount.Password);
        }

        public void DeleteAdditionalAddress(int sequenceNumber)
        {
            manager.Navigator.GoToAddressBookPage();

            // scroll to the table with additional addresses
            IWebElement table = driver.FindElement(By.CssSelector("div[class='table-wrapper additional-addresses']"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(table).Perform();

            driver.FindElement(By.CssSelector("div[class='table-wrapper additional-addresses']")).FindElement(By.TagName("tbody")).FindElements(By.TagName("tr"))[sequenceNumber].FindElement(By.CssSelector("td[data-th='Actions']")).FindElements(By.TagName("a"))[1].Click();
            driver.FindElement(By.CssSelector("button[class='action-primary action-accept']")).SendKeys(Keys.Enter);
        }

        internal void EditAdditionalAddress(int sequenceNumber, AddressData address)
        {
            manager.Navigator.GoToAddressBookPage();

            // scroll to the table with additional addresses
            IWebElement table = driver.FindElement(By.CssSelector("div[class='table-wrapper additional-addresses']"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(table);
            actions.Perform();

            driver.FindElement(By.CssSelector("div[class='table-wrapper additional-addresses']")).FindElement(By.TagName("tbody")).FindElements(By.TagName("tr"))[sequenceNumber].FindElement(By.CssSelector("td[data-th='Actions']")).FindElements(By.TagName("a"))[0].Click();

            // wait
            new WebDriverWait(driver, TimeSpan.FromSeconds(30)).Until(element => element.FindElement(By.CssSelector("span[class='base'][data-ui-id='page-title-wrapper']")).Text == "Edit Address");

            // fill form

            // phone number
            driver.FindElement(By.Id("telephone")).Clear();
            driver.FindElement(By.Id("telephone")).SendKeys(address.PhoneNumber);

            // street address
            driver.FindElement(By.Id("street_1")).Clear();
            driver.FindElement(By.Id("street_1")).SendKeys(address.StreetAddress);

            // city
            driver.FindElement(By.Id("city")).Clear();
            driver.FindElement(By.Id("city")).SendKeys(address.City);

            // wait
            new WebDriverWait(driver, TimeSpan.FromSeconds(60)).Until(element => element.FindElement(By.CssSelector("select[id='region_id'][name='region_id']")).Displayed);

            // state
            IWebElement scrollView = driver.FindElement(By.CssSelector("select[id='region_id'][name='region_id']"));
            Actions action = new Actions(driver);
            action.MoveToElement(scrollView).Click().Perform();
            var number = driver.FindElement(By.Id("region_id")).FindElements(By.TagName("option")).Where(element => element.Text.ToLower() == address.State.ToLower()).First().GetAttribute("value");
            new SelectElement(driver.FindElement(By.Id("region_id"))).SelectByText(address.State);
            driver.FindElement(By.CssSelector($"option[value=\"{number}\"]")).Click();

            // zip code
            driver.FindElement(By.Id("zip")).Clear();
            driver.FindElement(By.Id("zip")).SendKeys(address.Zip);

            driver.FindElement(By.CssSelector("button[data-action='save-address']")).Click();
        }

        public bool CheckIfHasDefaultAddress()
        {
            return driver.FindElement(By.CssSelector("div[class='box box-billing-address']")).FindElement(By.TagName("address")).Text == "You have not set a default billing address.";
        }

        public string GetDefaultAddress()
        {
            manager.Navigator.GoToAccountPage();
            string address = driver.FindElement(By.CssSelector("div[class='box box-billing-address']")).FindElement(By.TagName("address")).Text;
            return address;
        }

        public void AddAdditionalAddress(AddressData address)
        {
            manager.Navigator.GoToEditAddressPage();

            // phone number
            driver.FindElement(By.Id("telephone")).Clear();
            driver.FindElement(By.Id("telephone")).SendKeys(address.PhoneNumber);

            // street address
            driver.FindElement(By.Id("street_1")).Clear();
            driver.FindElement(By.Id("street_1")).SendKeys(address.StreetAddress);

            // city
            driver.FindElement(By.Id("city")).Clear();
            driver.FindElement(By.Id("city")).SendKeys(address.City);

            // wait
            new WebDriverWait(driver, TimeSpan.FromSeconds(60)).Until(element => element.FindElement(By.CssSelector("select[id='region_id'][name='region_id']")).Displayed);

            // state
            IWebElement scrollView = driver.FindElement(By.CssSelector("select[id='region_id'][name='region_id']"));
            Actions act = new Actions(driver);
            act.MoveToElement(scrollView).Click().Perform();
            var number = driver.FindElement(By.Id("region_id")).FindElements(By.TagName("option")).Where(element => element.Text.ToLower() == address.State.ToLower()).First().GetAttribute("value");
            new SelectElement(driver.FindElement(By.Id("region_id"))).SelectByText(address.State);
            driver.FindElement(By.CssSelector($"option[value=\"{number}\"]")).Click();

            // zip code
            driver.FindElement(By.Id("zip")).Clear();
            driver.FindElement(By.Id("zip")).SendKeys(address.Zip);

            // country
            //driver.FindElement(By.Id("country")).FindElements(By.TagName("option")).Where(element => element.Text == address.Country).First().Click();

            driver.FindElement(By.CssSelector("button[data-action='save-address']")).Click();
        }

        public bool CheckIfAdditionalAddressPresent()
        {
            try
            {
                return driver.FindElement(By.CssSelector("div[class='block block-addresses-list'] > div[class='block-content'] > p[class='empty']")).Text == "You have no other address entries in your address book.";
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public List<AddressData> GetAdditionalAddresses()
        {
            manager.Navigator.GoToAddressBookPage();
            if (CheckIfAdditionalAddressPresent())
            {
                List<AddressData> emptyList = new List<AddressData>();
                return emptyList;
            }
            List<AddressData> additionalAddressesList = new List<AddressData>();
            ICollection<IWebElement> additionalAddresses = driver.FindElement(By.CssSelector("div[class='table-wrapper additional-addresses']")).FindElement(By.TagName("tbody")).FindElements(By.TagName("tr"));
            for (int i = 0; i < additionalAddresses.Count(); i++)
            {
                AddressData additionalAddress = new AddressData()
                {
                    Firstname = driver.FindElement(By.CssSelector("td[data-th='First Name']")).Text,
                    Lastname = driver.FindElement(By.CssSelector("td[data-th='Last Name']")).Text,
                    StreetAddress = driver.FindElement(By.CssSelector("td[data-th='Street Address']")).Text,
                    City = driver.FindElement(By.CssSelector("td[data-th='City']")).Text,
                    Country = driver.FindElement(By.CssSelector("td[data-th='Country']")).Text,
                    State = driver.FindElement(By.CssSelector("td[data-th='State']")).Text,
                    Zip = driver.FindElement(By.CssSelector("td[data-th='Zip/Postal Code']")).Text,
                    PhoneNumber = driver.FindElement(By.CssSelector("td[data-th='Phone']")).Text
                };
                additionalAddressesList.Add(additionalAddress);
            }
            return additionalAddressesList;
        }
    }
}
