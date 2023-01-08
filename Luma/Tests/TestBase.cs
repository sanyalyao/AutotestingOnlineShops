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
        private XElement loadXmlFile = XElement.Load(@"Luma\ClothesData.xml");

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

        public string GetCategoryTops(string sex)
        {
            List<string> categories = new List<string>();
            if (sex.ToLower() == "male")
            {
                IEnumerable<XElement> elements = loadXmlFile.Element("MenClothes").Element("TopsMan").Element("CategoryTops").Descendants("Category");
                foreach (XElement element in elements)
                {
                    categories.Add(element.Value);
                }
                string category = categories[rnd.Next(categories.Count())];
                return category;
            }
            else
            {
                IEnumerable<XElement> elements = loadXmlFile.Element("WomenClothes").Element("TopsWoman").Element("CategoryTops").Descendants("Category");
                foreach (XElement element in elements)
                {
                    categories.Add(element.Value);
                }
                string category = categories[rnd.Next(categories.Count())];
                return category;
            }
        }

        public string GetStyleTops(string sex)
        {
            List<string> styles = new List<string>();
            if (sex.ToLower() == "male")
            {
                IEnumerable<XElement> elements = loadXmlFile.Element("MenClothes").Element("TopsMan").Element("StyleTops").Descendants("Style");
                foreach (XElement element in elements)
                {
                    styles.Add(element.Value);
                }
                string style = styles[rnd.Next(styles.Count())];
                return style;
            }
            else
            {
                IEnumerable<XElement> elements = loadXmlFile.Element("WomenClothes").Element("TopsWoman").Element("StyleTops").Descendants("Style");
                foreach (XElement element in elements)
                {
                    styles.Add(element.Value);
                }
                string style = styles[rnd.Next(styles.Count())];
                return style;
            }
        }

        public string GetSize(string sex)
        {
            List<string> sizes = new List<string>();
            if (sex.ToLower() == "male")
            {
                IEnumerable<XElement> elements = loadXmlFile.Element("MenClothes").Element("TopsMan").Element("SizeTops").Descendants("Size");
                foreach (XElement element in elements)
                {
                    sizes.Add(element.Value);
                }
                string size = sizes[rnd.Next(sizes.Count())];
                return size;
            }
            else
            {
                IEnumerable<XElement> elements = loadXmlFile.Element("WomenClothes").Element("TopsWoman").Element("SizeTops").Descendants("Size");
                foreach (XElement element in elements)
                {
                    sizes.Add(element.Value);
                }
                string size = sizes[rnd.Next(sizes.Count())];
                return size;
            }
        }

        public string GetColor(string sex)
        {
            List<string> colors = new List<string>();
            if (sex.ToLower() == "male")
            {
                IEnumerable<XElement> elements = loadXmlFile.Element("MenClothes").Element("TopsMan").Element("ColorTops").Descendants("Color");
                foreach (XElement element in elements)
                {
                    colors.Add(element.Value);
                }
                string color = colors[rnd.Next(colors.Count())];
                return color;
            }
            else
            {
                IEnumerable<XElement> elements = loadXmlFile.Element("WomenClothes").Element("TopsWoman").Element("ColorTops").Descendants("Color");
                foreach (XElement element in elements)
                {
                    colors.Add(element.Value);
                }
                string color = colors[rnd.Next(colors.Count())];
                return color;
            }
        }

        public string GetMaterial(string sex)
        {
            List<string> materials = new List<string>();
            if (sex.ToLower() == "male")
            {
                IEnumerable<XElement> elements = loadXmlFile.Element("MenClothes").Element("TopsMan").Element("MaterialTops").Descendants("Material");
                foreach (XElement element in elements)
                {
                    materials.Add(element.Value);
                }
                string material = materials[rnd.Next(materials.Count())];
                return material;
            }
            else
            {
                IEnumerable<XElement> elements = loadXmlFile.Element("WomenClothes").Element("TopsWoman").Element("MaterialTops").Descendants("Material");
                foreach (XElement element in elements)
                {
                    materials.Add(element.Value);
                }
                string material = materials[rnd.Next(materials.Count())];
                return material;
            }
        }
    }
}
