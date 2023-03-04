using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace AutotestingOnlineShops.Luma
{
    public class TestBase
    {
        protected Manager app;
        private Random rnd = new Random();
        protected string credentialsCurrentAccount = "account_credentials.json";
        protected string accountWithoutDefaultAddress = "account_without_default_address.json";
        protected string accountWithDefaultAddress = "account_with_default_address.json";

        [SetUp]
        public void SetupApplicationManager()
        {
            app = Manager.GetInstance();
        }

        public string GenerateRandomString(int length)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[rnd.Next(s.Length)]).ToArray());
        }

        public string GenerateRandomEmail(int length)
        {
            string chars = "abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[rnd.Next(s.Length)]).ToArray()) + "@google.com";
        }

        public string GenerateRandomPassword()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%";
            return new string(Enumerable.Repeat(chars, 16).Select(s => s[rnd.Next(s.Length)]).ToArray());
        }

        public string GenerateRandomPhoneNumber()
        {
            StringBuilder builder = new StringBuilder();
            List<string> characters = new List<string>()
            {
                "",
                "-",
                " ",
                "(",
                ")"
            };
            int randomCharacter = rnd.Next(characters.Count);
            builder.Append(characters[randomCharacter]);
            builder.Append(rnd.Next(100, 1000));
            builder.Append(characters[randomCharacter]);
            builder.Append(rnd.Next(100, 1000));
            builder.Append(characters[randomCharacter]);
            builder.Append(rnd.Next(10, 100));
            builder.Append(characters[randomCharacter]);
            builder.Append(rnd.Next(10, 100));
            return $"+7{builder}";
        }

        public string GetRandomState()
        {
            List<string> states = File.ReadAllLines("Countries.txt").ToList();
            string state = states[rnd.Next(1, states.Count - 1)];
            return state;
        }

        public string GenerateRandomZipCode()
        {
            string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, 5).Select(s => s[rnd.Next(s.Length)]).ToArray());
        }

        public string GetRandomCountry()
        {
            List<string> countries = File.ReadAllLines("Countries.txt").ToList();
            return countries[rnd.Next(countries.Count)];
        }
    }
}
