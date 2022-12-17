using NUnit.Framework;
using System;
using System.Linq;

namespace AutotestingOnlineShops.Luma
{
    public class TestBase
    {
        protected Manager app;
        protected AccountData defaultAccount = new AccountData()
        {
            FirstName = "dfg",
            LastName = "dfg",
            Email = "sdfsdf@google.com",
            Password = "jkhgHJG465%$^"
        };
        protected Random rnd = new Random();

        [SetUp]
        public void SetupApplicationManager()
        {
            app = Manager.GetInstance();
        }

        public string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[rnd.Next(s.Length)]).ToArray());
        }

        public string GenerateRandomEmail(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[rnd.Next(s.Length)]).ToArray()) + "@google.com";
        }

        public string GenerateRandomPassword(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[rnd.Next(s.Length)]).ToArray());
        }
    }
}
