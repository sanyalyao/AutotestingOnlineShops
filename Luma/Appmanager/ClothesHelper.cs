using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Linq;
using System.Collections.Generic;
using OpenQA.Selenium.Interactions;
using System.Text.RegularExpressions;
using System.Threading;

namespace AutotestingOnlineShops.Luma
{
    public class ClothesHelper : MainHelper
    {
        private string baseURL;
        private string womenTopsURL;

        public ClothesHelper(Manager manager, string baseURL, string womenTopsURL) : base(manager)
        {
            this.baseURL = baseURL;
            this.womenTopsURL = womenTopsURL;
        }

        private void GoToURL(string sex, string typeOfClothes)
        {
            switch (sex.ToLower())
            {
                case "male":
                    {
                        break;
                    }
                case "female":
                    {
                        switch (typeOfClothes.ToLower())
                        {
                            case "tops":
                                {
                                    driver.Navigate().GoToUrl(baseURL + womenTopsURL);
                                    new WebDriverWait(driver, TimeSpan.FromSeconds(15)).Until(element => element.FindElement(By.CssSelector("span[class='base'][data-ui-id='page-title-wrapper']")).Text == "Tops");
                                    break;
                                }
                        }
                        break;
                    }
            }
        }

        private ICollection<IWebElement> GetShoppingOptions()
        {
            ICollection<IWebElement> optionWebElement = driver.FindElement(By.Id("narrow-by-list")).FindElements(By.CssSelector("div[data-role='collapsible']"));
            return optionWebElement;
        }

        public void ChooseSingleOption(ClothesData clothes, string sex, string typeOfClothes, string option)
        {
            GoToURL(sex, typeOfClothes);
            if (option.ToLower() == "category")
            {
                ChooseCategory(clothes, sex, option, typeOfClothes);
            }
            if (option.ToLower() == "style")
            {
                ChooseStyle(clothes, sex, option, typeOfClothes);
            }
            if (option.ToLower() == "size")
            {
                ChooseSize(clothes, sex, option, typeOfClothes);
            }
            if (option.ToLower() == "price")
            {
                ChoosePrice(clothes, sex, option, typeOfClothes);
            }
            if (option.ToLower() == "color")
            {
                ChooseColor(clothes, sex, option, typeOfClothes);
            }
            if (option.ToLower() == "material")
            {
                ChooseMaterial(clothes, sex, option, typeOfClothes);
            }
            if (option.ToLower() == "eco collection")
            {
                ChooseEcoCollection(clothes, sex, option, typeOfClothes);
            }
            if (option.ToLower() == "performance fabric")
            {
                ChoosePerformanceFabric(clothes, sex, option, typeOfClothes);
            }
            if (option.ToLower() == "erin recommends") 
            {
                ChooseErinRecommends(clothes, sex, option, typeOfClothes);
            }
            if (option.ToLower() == "new")
            {
                ChooseNew(clothes, sex, option, typeOfClothes);
            }
            if (option.ToLower() == "sale")
            {
                ChooseSale(clothes, sex, option, typeOfClothes);
            }
            if (option.ToLower() == "pattern")
            {
                ChoosePattern(clothes, sex, option, typeOfClothes);
            }
            if (option.ToLower() == "climate")
            {
                ChooseClimate(clothes, sex, option, typeOfClothes);
            }
        }

        // get count of clothes after choosing some shopping option
        public int GetCountOfClothes()
        {
            if (driver.FindElement(By.Id("toolbar-amount")).Text.Contains("of")) // if more than one page of products
            {
                IWebElement productsGrid = driver.FindElement(By.CssSelector("div[class='products wrapper grid products-grid']")).FindElement(By.CssSelector("ol[class='products list items product-items']"));
                List<IWebElement> listOfProducts = productsGrid.FindElements(By.CssSelector("li[class='item product product-item']")).ToList();
                do
                {
                    driver.FindElements(By.CssSelector("a[class='action  next']"))[1].Click();
                    IWebElement anotherProductsGrid = driver.FindElement(By.CssSelector("div[class='products wrapper grid products-grid']")).FindElement(By.CssSelector("ol[class='products list items product-items']"));
                    List<IWebElement> newListOfProducts = anotherProductsGrid.FindElements(By.CssSelector("li[class='item product product-item']")).ToList();
                    listOfProducts.AddRange(newListOfProducts);
                    Thread.Sleep(5);
                }
                while (driver.FindElements(By.CssSelector("a[class='action  next']")).Count != 0);
                return listOfProducts.Count;
            }
            else // if one page of products
            {
                IWebElement productsGrid = driver.FindElement(By.CssSelector("div[class='products wrapper grid products-grid']")).FindElement(By.CssSelector("ol[class='products list items product-items']"));
                List<IWebElement> listOfProducts = productsGrid.FindElements(By.CssSelector("li[class='item product product-item']")).ToList();
                return listOfProducts.Count();
            }
        }

        // get count of clothes from toolbar after choosing some shopping option
        public int GetCountOfItemsFromToolbar()
        {
            if (driver.FindElement(By.Id("toolbar-amount")).Text.Contains("of"))
            {
                return Int32.Parse(driver.FindElement(By.Id("toolbar-amount")).Text.Split( new string[] { "of" }, StringSplitOptions.None)[1]);
            }
            else
            {
                return Int32.Parse(driver.FindElement(By.Id("toolbar-amount")).FindElement(By.ClassName("toolbar-number")).Text);
            }
        }

        private void WaitCollapsedList(string option)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(15)).Until(element => element.FindElement(By.CssSelector("div[data-role='title'][class='filter-options-title']")).GetAttribute("aria-selected") == "false"); // wait because when you open page, menu of shopping options is not collapsed. after several seconds it will be collapsed automatically

            GetShoppingOptions().Where(element => element.FindElement(By.ClassName("filter-options-title")).Text.ToLower() == option.ToLower()).First().Click();
        }

        private void ChooseCategory(ClothesData clothes, string sex, string option, string typeOfClothes)
        {
            WaitCollapsedList(option);
            if (sex.ToLower() == "female")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    string category = clothes.WomenClothes.TopsWoman.CategoryTops.Category.ToLower();
                    ClickOption(option, category);
                }

                if (typeOfClothes.ToLower() == "bottoms")
                {
                    string category = clothes.WomenClothes.BottomsWoman.CategoryBottoms.Category.ToLower();
                    ClickOption(option, category);
                }
            }

            if (sex.ToLower() == "male")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    string category = clothes.MenClothes.TopsMan.CategoryTops.Category.ToLower();
                    ClickOption(option, category);
                }

                if (typeOfClothes.ToLower() == "bottoms")
                {
                    string category = clothes.MenClothes.BottomsMan.CategoryBottoms.Category.ToLower();
                    ClickOption(option, category);
                }
            }
        }

        private void ChooseStyle(ClothesData clothes, string sex, string option, string typeOfClothes)
        {
            WaitCollapsedList(option);
            if (sex.ToLower() == "female")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    string style = clothes.WomenClothes.TopsWoman.StyleTops.Style.ToLower();
                    ClickOption(option, style);
                }
                if (typeOfClothes.ToLower() == "bottoms")
                {
                    string style = clothes.WomenClothes.BottomsWoman.StyleBottoms.Style.ToLower();
                    ClickOption(option, style);
                }
            }
            if (sex.ToLower() == "male")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    string style = clothes.MenClothes.TopsMan.StyleTops.Style.ToLower();
                    ClickOption(option, style);
                }
                if (typeOfClothes.ToLower() == "bottoms")
                {
                    string style = clothes.MenClothes.BottomsMan.StyleBottoms.Style.ToLower();
                    ClickOption(option, style);
                }
            }
        }

        private void ChooseSize(ClothesData clothes, string sex, string option, string typeOfClothes)
        {
            WaitCollapsedList(option);
            if (sex.ToLower() == "female")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    string size = clothes.WomenClothes.TopsWoman.SizeTops.Size.ToLower();
                    ClickSize(option, size);
                }
                if (typeOfClothes.ToLower() == "bottoms")
                {
                    string size = clothes.WomenClothes.BottomsWoman.SizeBottoms.Size.ToLower();
                    ClickOption(option, size);
                }
            }
            if (sex.ToLower() == "male")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    string size = clothes.MenClothes.TopsMan.SizeTops.Size.ToLower();
                    ClickOption(option, size);
                }
                if (typeOfClothes.ToLower() == "bottoms")
                {
                    string size = clothes.MenClothes.BottomsMan.SizeBottoms.Size.ToLower();
                    ClickOption(option, size);
                }
            }
        }

        private void ChoosePrice(ClothesData clothes, string sex, string option, string typeOfClothes)
        {
            WaitCollapsedList(option);
            if (sex.ToLower() == "female")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    string price = clothes.WomenClothes.TopsWoman.PriceTops.Price.ToLower();
                    ClickOption(option, price);
                }
                if (typeOfClothes.ToLower() == "bottoms")
                {
                    string price = clothes.WomenClothes.BottomsWoman.PriceBottoms.Price.ToLower();
                    ClickOption(option, price);
                }
            }
            if (sex.ToLower() == "male")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    string price = clothes.MenClothes.TopsMan.PriceTops.Price.ToLower();
                    ClickOption(option, price);
                }
                if (typeOfClothes.ToLower() == "bottoms")
                {
                    string price = clothes.MenClothes.BottomsMan.PriceBottoms.Price.ToLower();
                    ClickOption(option, price);
                }
            }
        }

        private void ChooseColor(ClothesData clothes, string sex, string option, string typeOfClothes)
        {
            WaitCollapsedList(option);
            if (sex.ToLower() == "female")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    string color = clothes.WomenClothes.TopsWoman.ColorTops.Color.ToLower();
                    ClickOption(option, color);
                }
                if (typeOfClothes.ToLower() == "bottoms")
                {
                    string color = clothes.WomenClothes.BottomsWoman.ColorBottoms.Color.ToLower();
                    ClickOption(option, color);
                }
            }
            if (sex.ToLower() == "male")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    string color = clothes.MenClothes.TopsMan.ColorTops.Color.ToLower();
                    ClickOption(option, color);
                }
                if (typeOfClothes.ToLower() == "bottoms")
                {
                    string color = clothes.MenClothes.BottomsMan.ColorBottoms.Color.ToLower();
                    ClickOption(option, color);
                }
            }
        }

        private void ChooseMaterial(ClothesData clothes, string sex, string option, string typeOfClothes)
        {
            WaitCollapsedList(option);
            if (sex.ToLower() == "female")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    string material = clothes.WomenClothes.TopsWoman.MaterialTops.Material.ToLower();
                    ClickOption(option, material);
                }
                if (typeOfClothes.ToLower() == "bottoms")
                {
                    string material = clothes.WomenClothes.BottomsWoman.MaterialBottoms.Material.ToLower();
                    ClickOption(option, material);
                }
            }
            if (sex.ToLower() == "male")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    string material = clothes.MenClothes.TopsMan.MaterialTops.Material.ToLower();
                    ClickOption(option, material);
                }
                if (typeOfClothes.ToLower() == "bottoms")
                {
                    string material = clothes.MenClothes.BottomsMan.MaterialBottoms.Material.ToLower();
                    ClickOption(option, material);
                }
            }
        }

        private void ChooseEcoCollection(ClothesData clothes, string sex, string option, string typeOfClothes)
        {
            WaitCollapsedList(option);
            if (sex.ToLower() == "female")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    string ecoCollection = clothes.WomenClothes.TopsWoman.EcoCollection.ToLower();
                    ClickOption(option, ecoCollection);
                }
                if (typeOfClothes.ToLower() == "bottoms")
                {
                    string ecoCollection = clothes.WomenClothes.BottomsWoman.EcoCollection.ToLower();
                    ClickOption(option, ecoCollection);
                }
            }
            if (sex.ToLower() == "male")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    string ecoCollection = clothes.MenClothes.TopsMan.EcoCollection.ToLower();
                    ClickOption(option, ecoCollection);
                }
                if (typeOfClothes.ToLower() == "bottoms")
                {
                    string ecoCollection = clothes.MenClothes.BottomsMan.EcoCollection.ToLower();
                    ClickOption(option, ecoCollection);
                }
            }
        }

        private void ChoosePerformanceFabric(ClothesData clothes, string sex, string option, string typeOfClothes)
        {
            WaitCollapsedList(option);
            if (sex.ToLower() == "female")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    string perfomanceFabric = clothes.WomenClothes.TopsWoman.PerformanceFabric.ToLower();
                    ClickOption(option, perfomanceFabric);
                }
                if (typeOfClothes.ToLower() == "bottoms")
                {
                    string perfomanceFabric = clothes.WomenClothes.BottomsWoman.PerformanceFabric.ToLower();
                    ClickOption(option, perfomanceFabric);
                }
            }
            if (sex.ToLower() == "male")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    string perfomanceFabric = clothes.MenClothes.TopsMan.PerformanceFabric.ToLower();
                    ClickOption(option, perfomanceFabric);
                }
                if (typeOfClothes.ToLower() == "bottoms")
                {
                    string perfomanceFabric = clothes.MenClothes.BottomsMan.PerformanceFabric.ToLower();
                    ClickOption(option, perfomanceFabric);
                }
            }
        }

        private void ChooseErinRecommends(ClothesData clothes, string sex, string option, string typeOfClothes)
        {
            WaitCollapsedList(option);
            if (sex.ToLower() == "female")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    string erinRecommends = clothes.WomenClothes.TopsWoman.ErinRecommends.ToLower();
                    ClickOption(option, erinRecommends);
                }
                if (typeOfClothes.ToLower() == "bottoms")
                {
                    string erinRecommends = clothes.WomenClothes.BottomsWoman.ErinRecommends.ToLower();
                    ClickOption(option, erinRecommends);
                }
            }
            if (sex.ToLower() == "male")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    string erinRecommends = clothes.MenClothes.TopsMan.ErinRecommends.ToLower();
                    ClickOption(option, erinRecommends);
                }
                if (typeOfClothes.ToLower() == "bottoms")
                {
                    string erinRecommends = clothes.MenClothes.BottomsMan.ErinRecommends.ToLower();
                    ClickOption(option, erinRecommends);
                }
            }
        }

        private void ChooseNew(ClothesData clothes, string sex, string option, string typeOfClothes)
        {
            WaitCollapsedList(option);
            if (sex.ToLower() == "female")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    string optionNew = clothes.WomenClothes.TopsWoman.New.ToLower();
                    ClickOption(option, optionNew);
                }
                if (typeOfClothes.ToLower() == "bottoms")
                {
                    string optionNew = clothes.WomenClothes.BottomsWoman.New.ToLower();
                    ClickOption(option, optionNew);
                }
            }
            if (sex.ToLower() == "male")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    string optionNew = clothes.MenClothes.TopsMan.New.ToLower();
                    ClickOption(option, optionNew);
                }
                if (typeOfClothes.ToLower() == "bottoms")
                {
                    string optionNew = clothes.MenClothes.BottomsMan.New.ToLower();
                    ClickOption(option, optionNew);
                }
            }
        }

        private void ChooseSale(ClothesData clothes, string sex, string option, string typeOfClothes)
        {
            WaitCollapsedList(option);
            if (sex.ToLower() == "female")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    string sale = clothes.WomenClothes.TopsWoman.Sale.ToLower();
                    ClickOption(option, sale);
                }
                if (typeOfClothes.ToLower() == "bottoms")
                {
                    string sale = clothes.WomenClothes.BottomsWoman.Sale.ToLower();
                    ClickOption(option, sale);
                }
            }
            if (sex.ToLower() == "male")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    string sale = clothes.MenClothes.TopsMan.Sale.ToLower();
                    ClickOption(option, sale);
                }
                if (typeOfClothes.ToLower() == "bottoms")
                {
                    string sale = clothes.MenClothes.BottomsMan.Sale.ToLower();
                    ClickOption(option, sale);
                }
            }
        }

        private void ChoosePattern(ClothesData clothes, string sex, string option, string typeOfClothes)
        {
            WaitCollapsedList(option);
            if (sex.ToLower() == "female")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    string pattern = clothes.WomenClothes.TopsWoman.PatternTops.Pattern.ToLower();
                    ClickOption(option, pattern);
                }
                if (typeOfClothes.ToLower() == "bottoms")
                {
                    string pattern = clothes.WomenClothes.BottomsWoman.PatternBottoms.Pattern.ToLower();
                    ClickOption(option, pattern);
                }
            }
            if (sex.ToLower() == "male")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    string pattern = clothes.MenClothes.TopsMan.PatternTops.Pattern.ToLower();
                    ClickOption(option, pattern);
                }
                if (typeOfClothes.ToLower() == "bottoms")
                {
                    string pattern = clothes.MenClothes.BottomsMan.PatternBottoms.Pattern.ToLower();
                    ClickOption(option, pattern);
                }
            }
        }

        private void ChooseClimate(ClothesData clothes, string sex, string option, string typeOfClothes)
        {
            WaitCollapsedList(option);
            if (sex.ToLower() == "female")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    string climate = clothes.WomenClothes.TopsWoman.ClimateTops.Climate.ToLower();
                    ClickOption(option, climate);
                }
                if (typeOfClothes.ToLower() == "bottoms")
                {
                    string climate = clothes.WomenClothes.BottomsWoman.ClimateBottoms.Climate.ToLower();
                    ClickOption(option, climate);
                }
            }
            if (sex.ToLower() == "male")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    string climate = clothes.MenClothes.TopsMan.ClimateTops.Climate.ToLower();
                    ClickOption(option, climate);
                }
                if (typeOfClothes.ToLower() == "bottoms")
                {
                    string climate = clothes.MenClothes.BottomsMan.ClimateBottoms.Climate.ToLower();
                    ClickOption(option, climate);
                }
            }
        }

        private void ClickOption(string option, string valueFromOption)
        {
            if (option.ToLower() != "color")
            {
                IWebElement itemsElement = GetShoppingOptions().Where(element => element.FindElement(By.ClassName("filter-options-title")).Text.ToLower() == option.ToLower()).First().FindElement(By.CssSelector("ol[class='items']")); // list of items with links for each category
                if (option.ToLower() == "price")
                {
                    var itemsWithPrices = itemsElement.FindElements(By.CssSelector("li[class='item']")).Select(element => element.FindElement(By.TagName("a")).FindElements(By.CssSelector("span[class='price']"))).ToList();
                    IWebElement selectedPrice = null;
                    // choose the price
                    for (int i = 0; i < itemsWithPrices.Count(); i++)
                    {
                        for (int y = 0; y < itemsWithPrices[i].Count() - 1; y++)
                        {
                            if ($"{itemsWithPrices[i][y].Text} - {itemsWithPrices[i][y + 1].Text}" == valueFromOption)
                            {
                                selectedPrice = itemsWithPrices[i][y];
                            }
                        }
                    }
                    selectedPrice.Click(); // click item with link for choosen category
                }
                else
                {
                    itemsElement.FindElements(By.CssSelector("li[class='item']")).Where(element => Regex.Split(element.FindElement(By.TagName("a")).Text.ToLower(), @"\d")[0].Trim() == valueFromOption).First().FindElement(By.TagName("a")).Click(); // click item with link for choosen category
                }
            }
            if (option.ToLower() == "color")
            {
                IWebElement itemsElement = GetShoppingOptions().Where(element => element.FindElement(By.ClassName("filter-options-title")).Text.ToLower() == option.ToLower()).First().FindElement(By.CssSelector("div[class='swatch-attribute swatch-layered color']")); // list of colors

                ICollection<IWebElement> webElementsColors = itemsElement.FindElement(By.CssSelector("div[class='swatch-attribute-options clearfix']")).FindElements(By.TagName("a")); // list of items with links

                webElementsColors.Where(element => element.FindElement(By.TagName("div")).GetAttribute("option-label").ToLower() == valueFromOption.ToLower()).First().FindElement(By.TagName("div")).Click(); // click choosen color
            }
            // wait
            new WebDriverWait(driver, TimeSpan.FromSeconds(15)).Until(element => element.FindElement(By.ClassName("filter-current")).FindElements(By.CssSelector("li[class='item']")).Where(item => item.FindElement(By.ClassName("filter-value")).Text.ToLower() == valueFromOption));
        }

        private void ClickSize(string option, string kindOfOptions)
        {
            IWebElement itemsElement = GetShoppingOptions().Where(element => element.FindElement(By.ClassName("filter-options-title")).Text.ToLower() == option.ToLower()).First().FindElement(By.CssSelector("div[class='swatch-attribute-options clearfix']")); // list of items with links for each category
            itemsElement.FindElements(By.TagName("a")).Where(element => element.GetAttribute("aria-label").ToLower().Trim() == kindOfOptions).First().FindElement(By.TagName("div")).Click();  // click item with link for choosen category
        }
    }
}
