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
    public class EditAddresses : TestBase
    {
        [Test]
        public void AddDefaultAddress()
        {
            AccountData accountWithoutAddress = app.Credentials.ReadAccountCredentials(accountWithoutDefaultAddress);
            app.Login.SignIn(accountWithoutAddress.Email, accountWithoutAddress.Password);
            AddressData newAddress = new AddressData()
            {
                Firstname = accountWithoutAddress.FirstName,
                Lastname = accountWithoutAddress.LastName,
                CompanyName = GenerateRandomString(5),
                PhoneNumber = GenerateRandomPhoneNumber(),
                StreetAddress = GenerateRandomString(10),
                City = GenerateRandomString(10),
                State = GetRandomState(),
                Zip = GenerateRandomZipCode(),
                Country = "United States"
            };
            app.Account.AddDefaultAddress(newAddress);
            string defaultAddress = app.Account.GetDefaultAddress();
            string currentAddress = newAddress.FullAddress();
            app.Credentials.SaveAccountWithDefaultAddress(accountWithoutAddress);
            Assert.AreEqual(defaultAddress, currentAddress);
        }

        [Test]
        public void AddAdditionalAddress()
        {

        }
    }
}
