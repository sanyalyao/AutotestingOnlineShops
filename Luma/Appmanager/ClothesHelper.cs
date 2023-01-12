using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Linq;
using System.Collections.Generic;
using OpenQA.Selenium.Interactions;
using System.Text.RegularExpressions;

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
                                    new WebDriverWait(driver, TimeSpan.FromSeconds(15)).Until(element => element.FindElement(By.CssSelector("div[data-role='title'][class='filter-options-title']")).GetAttribute("aria-selected") == "false");
                                    break;
                                }
                        }
                        break;
                    }
            }
        }

        private IWebElement GetShoppingOptions()
        {
            IWebElement shoppingOptions = driver.FindElement(By.Id("narrow-by-list"));
            return shoppingOptions;
        }

        public void ChooseOptions(ClothesData clothes, string sex, string typeOfClothes, string option)
        {
            GoToURL(sex, typeOfClothes);
            if (option.ToLower() == "category")
            {
                ChooseCategory(clothes, sex, option);
            }
        }

        private void ChooseCategory(ClothesData clothes, string sex, string option)
        {
            var category = clothes.WomenClothes.TopsWoman.CategoryTops.Category.ToLower();
            ICollection<IWebElement> optionWebElement = GetShoppingOptions().FindElements(By.CssSelector("div[data-role='collapsible']"));
            optionWebElement.Where(element => element.FindElement(By.ClassName("filter-options-title")).Text.ToLower() == option.ToLower()).First().Click();

            // wait because when you open page, menu of shopping options is not collapsed. after several seconds it will be collapsed automatically
            new WebDriverWait(driver, TimeSpan.FromSeconds(15)).Until(element => element.FindElement(By.CssSelector("div[data-role='title'][class='filter-options-title']")).GetAttribute("aria-selected") == "true");

            if (sex.ToLower() == "female")
            {
                IWebElement itemsElement = optionWebElement.Where(element => element.FindElement(By.ClassName("filter-options-title")).Text.ToLower() == option.ToLower()).First().FindElement(By.CssSelector("ol[class='items']")); // list of items with links for each category

                itemsElement.FindElements(By.CssSelector("li[class='item']")).Where(element => Regex.Split(element.FindElement(By.TagName("a")).Text.ToLower(), @"\d")[0].Trim() == category).First().FindElement(By.TagName("a")).Click(); // click item with link for choosen category

                // wait
                new WebDriverWait(driver, TimeSpan.FromSeconds(15)).Until(element => element.FindElement(By.ClassName("filter-current")).FindElements(By.CssSelector("li[class='item']")).Where(item => item.FindElement(By.ClassName("filter-value")).Text.ToLower() == category));
            }
        }
    }
}
