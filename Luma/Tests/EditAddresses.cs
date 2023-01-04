using System.Collections.Generic;
using System.Linq;
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
            app.Login.SignIn(accountWithoutAddress);
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
            string currentAddress = newAddress.FullDefaultAddress();
            app.Credentials.SaveAccountWithDefaultAddress(accountWithoutAddress);
            Assert.AreEqual(defaultAddress, currentAddress);
        }

        [Test]
        public void AddAdditionalAddress()
        {
            AccountData accountWithAddress = app.Credentials.ReadAccountCredentials(accountWithDefaultAddress);
            app.Login.SignIn(accountWithAddress);
            AddressData newAdditionalAddress = new AddressData()
            {
                Firstname = accountWithAddress.FirstName,
                Lastname = accountWithAddress.LastName,
                PhoneNumber = GenerateRandomPhoneNumber(),
                StreetAddress = GenerateRandomString(10),
                City = GenerateRandomString(10),
                State = GetRandomState(),
                Zip = GenerateRandomZipCode(),
                Country = "United States"
            };
            List<AddressData> oldAdditionalAddresses = app.Account.GetAdditionalAddresses();
            app.Account.AddAdditionalAddress(newAdditionalAddress);
            List<AddressData> newAdditionalAddresses = app.Account.GetAdditionalAddresses();
            oldAdditionalAddresses.Add(newAdditionalAddress);
            Assert.AreEqual(oldAdditionalAddresses.Count, newAdditionalAddresses.Count);
            Assert.Contains(newAdditionalAddress, newAdditionalAddresses);
        }

        [Test]
        public void DeleteAdditionalAddress()
        {
            AccountData accountWithAddress = app.Credentials.ReadAccountCredentials(accountWithDefaultAddress);
            app.Login.SignIn(accountWithAddress);
            if (app.Account.GetAdditionalAddresses().Count() == 0)
            {
                AddressData newAdditionalAddress = new AddressData()
                {
                    Firstname = accountWithAddress.FirstName,
                    Lastname = accountWithAddress.LastName,
                    PhoneNumber = GenerateRandomPhoneNumber(),
                    StreetAddress = GenerateRandomString(10),
                    City = GenerateRandomString(10),
                    State = GetRandomState(),
                    Zip = GenerateRandomZipCode(),
                    Country = "United States"
                };
                app.Account.AddAdditionalAddress(newAdditionalAddress);
            }
            List<AddressData> oldAdditionalAddresses = app.Account.GetAdditionalAddresses();
            app.Account.DeleteAdditionalAddress(0);
            List<AddressData> newAdditionalAddresses = app.Account.GetAdditionalAddresses();
            oldAdditionalAddresses.RemoveAt(0);
            Assert.AreEqual(oldAdditionalAddresses.Count, newAdditionalAddresses.Count);
        }

        [Test]
        public void EditAdditionalAddress()
        {
            AccountData accountWithAddress = app.Credentials.ReadAccountCredentials(accountWithDefaultAddress);
            app.Login.SignIn(accountWithAddress);
            if (app.Account.GetAdditionalAddresses().Count() == 0)
            {
                AddressData newAdditionalAddress = new AddressData()
                {
                    Firstname = accountWithAddress.FirstName,
                    Lastname = accountWithAddress.LastName,
                    PhoneNumber = GenerateRandomPhoneNumber(),
                    StreetAddress = GenerateRandomString(10),
                    City = GenerateRandomString(10),
                    State = GetRandomState(),
                    Zip = GenerateRandomZipCode(),
                    Country = "United States"
                };
                app.Account.AddAdditionalAddress(newAdditionalAddress);
            }
            AddressData editAdditionalAddress = new AddressData()
            {
                Firstname = accountWithAddress.FirstName,
                Lastname = accountWithAddress.LastName,
                PhoneNumber = GenerateRandomPhoneNumber(),
                StreetAddress = GenerateRandomString(10),
                City = GenerateRandomString(10),
                State = GetRandomState(),
                Zip = GenerateRandomZipCode(),
                Country = "United States"
            };
            List<AddressData> oldAdditionalAddresses = app.Account.GetAdditionalAddresses();
            app.Account.EditAdditionalAddress(0, editAdditionalAddress);
            List<AddressData> newAdditionalAddresses = app.Account.GetAdditionalAddresses();
            Assert.AreEqual(oldAdditionalAddresses.Count, newAdditionalAddresses.Count);
            Assert.Contains(editAdditionalAddress, newAdditionalAddresses);
        }
    }
}
