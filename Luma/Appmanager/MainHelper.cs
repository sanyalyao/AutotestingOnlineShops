using OpenQA.Selenium;
using System.Collections.Generic;
using System.Xml.Linq;
using System;
using System.Linq;

namespace AutotestingOnlineShops.Luma
{
    public class MainHelper
    {
        protected Manager manager;
        protected IWebDriver driver;
        private XElement loadXmlFile = XElement.Load(@"Luma\ClothesData.xml");
        private Random rnd = new Random();

        public MainHelper(Manager manager)
        {
            this.manager = manager;
            driver = manager.Driver;
        }

        public void Type(By locator, string text)
        {
            if (text != null)
            {
                driver.FindElement(locator).Clear();
                driver.FindElement(locator).SendKeys(text);
            }
        }

        public bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
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

        public string GetCategoryBottoms(string sex)
        {
            List<string> categories = new List<string>();
            if (sex.ToLower() == "male")
            {
                IEnumerable<XElement> elements = loadXmlFile.Element("MenClothes").Element("BottomsMan").Element("CategoryBottoms").Descendants("Category");
                foreach (XElement element in elements)
                {
                    categories.Add(element.Value);
                }
                string category = categories[rnd.Next(categories.Count())];
                return category;
            }
            else
            {
                IEnumerable<XElement> elements = loadXmlFile.Element("WomenClothes").Element("BottomsWoman").Element("CategoryBottoms").Descendants("Category");
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

        public string GetStyleBottoms(string sex)
        {
            List<string> styles = new List<string>();
            if (sex.ToLower() == "male")
            {
                IEnumerable<XElement> elements = loadXmlFile.Element("MenClothes").Element("BottomsMan").Element("StyleBottoms").Descendants("Style");
                foreach (XElement element in elements)
                {
                    styles.Add(element.Value);
                }
                string style = styles[rnd.Next(styles.Count())];
                return style;
            }
            else
            {
                IEnumerable<XElement> elements = loadXmlFile.Element("WomenClothes").Element("BottomsWoman").Element("StyleBottoms").Descendants("Style");
                foreach (XElement element in elements)
                {
                    styles.Add(element.Value);
                }
                string style = styles[rnd.Next(styles.Count())];
                return style;
            }
        }

        public string GetSizeTops(string sex)
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

        public string GetSizeBottoms(string sex)
        {
            List<string> sizes = new List<string>();
            if (sex.ToLower() == "male")
            {
                IEnumerable<XElement> elements = loadXmlFile.Element("MenClothes").Element("BottomsMan").Element("SizeBottoms").Descendants("Size");
                foreach (XElement element in elements)
                {
                    sizes.Add(element.Value);
                }
                string size = sizes[rnd.Next(sizes.Count())];
                return size;
            }
            else
            {
                IEnumerable<XElement> elements = loadXmlFile.Element("WomenClothes").Element("BottomsWoman").Element("SizeBottoms").Descendants("Size");
                foreach (XElement element in elements)
                {
                    sizes.Add(element.Value);
                }
                string size = sizes[rnd.Next(sizes.Count())];
                return size;
            }
        }

        public string GetPriceTops(string sex)
        {
            List<string> prices = new List<string>();
            if (sex.ToLower() == "male")
            {
                IEnumerable<XElement> elements = loadXmlFile.Element("MenClothes").Element("TopsMan").Element("PriceTops").Descendants("Price");
                foreach (XElement element in elements)
                {
                    prices.Add(element.Value);
                }
                string price = prices[rnd.Next(prices.Count())];
                return price;
            }
            else
            {
                IEnumerable<XElement> elements = loadXmlFile.Element("WomenClothes").Element("TopsWoman").Element("PriceTops").Descendants("Price");
                foreach (XElement element in elements)
                {
                    prices.Add(element.Value);
                }
                string price = prices[rnd.Next(prices.Count())];
                return price;
            }
        }

        public string GetPriceBottoms(string sex)
        {
            List<string> prices = new List<string>();
            if (sex.ToLower() == "male")
            {
                IEnumerable<XElement> elements = loadXmlFile.Element("MenClothes").Element("BottomsMan").Element("PriceBottoms").Descendants("Price");
                foreach (XElement element in elements)
                {
                    prices.Add(element.Value);
                }
                string price = prices[rnd.Next(prices.Count())];
                return price;
            }
            else
            {
                IEnumerable<XElement> elements = loadXmlFile.Element("WomenClothes").Element("BottomsWoman").Element("PriceBottoms").Descendants("Price");
                foreach (XElement element in elements)
                {
                    prices.Add(element.Value);
                }
                string price = prices[rnd.Next(prices.Count())];
                return price;
            }
        }

        public string GetColorTops(string sex)
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

        public string GetColorBottoms(string sex)
        {
            List<string> colors = new List<string>();
            if (sex.ToLower() == "male")
            {
                IEnumerable<XElement> elements = loadXmlFile.Element("MenClothes").Element("BottomsMan").Element("ColorBottoms").Descendants("Color");
                foreach (XElement element in elements)
                {
                    colors.Add(element.Value);
                }
                string color = colors[rnd.Next(colors.Count())];
                return color;
            }
            else
            {
                IEnumerable<XElement> elements = loadXmlFile.Element("WomenClothes").Element("BottomsWoman").Element("ColorBottoms").Descendants("Color");
                foreach (XElement element in elements)
                {
                    colors.Add(element.Value);
                }
                string color = colors[rnd.Next(colors.Count())];
                return color;
            }
        }

        public string GetMaterialTops(string sex)
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

        public string GetMaterialBottoms(string sex)
        {
            List<string> materials = new List<string>();
            if (sex.ToLower() == "male")
            {
                IEnumerable<XElement> elements = loadXmlFile.Element("MenClothes").Element("BottomsMan").Element("MaterialBottoms").Descendants("Material");
                foreach (XElement element in elements)
                {
                    materials.Add(element.Value);
                }
                string material = materials[rnd.Next(materials.Count())];
                return material;
            }
            else
            {
                IEnumerable<XElement> elements = loadXmlFile.Element("WomenClothes").Element("BottomsWoman").Element("MaterialBottoms").Descendants("Material");
                foreach (XElement element in elements)
                {
                    materials.Add(element.Value);
                }
                string material = materials[rnd.Next(materials.Count())];
                return material;
            }
        }

        public string GetPatternTops(string sex)
        {
            List<string> patterns = new List<string>();
            if (sex.ToLower() == "male")
            {
                IEnumerable<XElement> elements = loadXmlFile.Element("MenClothes").Element("TopsMan").Element("PatternTops").Descendants("Pattern");
                foreach (XElement element in elements)
                {
                    patterns.Add(element.Value);
                }
                string pattern = patterns[rnd.Next(patterns.Count())];
                return pattern;
            }
            else
            {
                IEnumerable<XElement> elements = loadXmlFile.Element("WomenClothes").Element("TopsWoman").Element("PatternTops").Descendants("Pattern");
                foreach (XElement element in elements)
                {
                    patterns.Add(element.Value);
                }
                string pattern = patterns[rnd.Next(patterns.Count())];
                return pattern;
            }
        }

        public string GetPatternBottoms(string sex)
        {
            List<string> patterns = new List<string>();
            if (sex.ToLower() == "male")
            {
                IEnumerable<XElement> elements = loadXmlFile.Element("MenClothes").Element("BottomsMan").Element("PatternBottoms").Descendants("Pattern");
                foreach (XElement element in elements)
                {
                    patterns.Add(element.Value);
                }
                string pattern = patterns[rnd.Next(patterns.Count())];
                return pattern;
            }
            else
            {
                IEnumerable<XElement> elements = loadXmlFile.Element("WomenClothes").Element("BottomsWoman").Element("PatternBottoms").Descendants("Pattern");
                foreach (XElement element in elements)
                {
                    patterns.Add(element.Value);
                }
                string pattern = patterns[rnd.Next(patterns.Count())];
                return pattern;
            }
        }

        public string GetClimateTops(string sex)
        {
            List<string> climates = new List<string>();
            if (sex.ToLower() == "male")
            {
                IEnumerable<XElement> elements = loadXmlFile.Element("MenClothes").Element("TopsMan").Element("ClimateTops").Descendants("Climate");
                foreach (XElement element in elements)
                {
                    climates.Add(element.Value);
                }
                string climate = climates[rnd.Next(climates.Count())];
                return climate;
            }
            else
            {
                IEnumerable<XElement> elements = loadXmlFile.Element("WomenClothes").Element("TopsWoman").Element("ClimateTops").Descendants("Climate");
                foreach (XElement element in elements)
                {
                    climates.Add(element.Value);
                }
                string climate = climates[rnd.Next(climates.Count())];
                return climate;
            }
        }

        public string GetClimateBottoms(string sex)
        {
            List<string> climates = new List<string>();
            if (sex.ToLower() == "male")
            {
                IEnumerable<XElement> elements = loadXmlFile.Element("MenClothes").Element("BottomsMan").Element("ClimateBottoms").Descendants("Climate");
                foreach (XElement element in elements)
                {
                    climates.Add(element.Value);
                }
                string climate = climates[rnd.Next(climates.Count())];
                return climate;
            }
            else
            {
                IEnumerable<XElement> elements = loadXmlFile.Element("WomenClothes").Element("BottomsWoman").Element("ClimateBottoms").Descendants("Climate");
                foreach (XElement element in elements)
                {
                    climates.Add(element.Value);
                }
                string climate = climates[rnd.Next(climates.Count())];
                return climate;
            }
        }

        public string GetYesOrNo()
        {
            string[] words = new string[] { "Yes","No" };
            return words[rnd.Next(0,2)];
        }
    }
}
