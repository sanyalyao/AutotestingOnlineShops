using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace AutotestingOnlineShops.Luma
{
    public class ClothesHelper : MainHelper
    {
        private string baseURL;
        private Random rnd = new Random();
        private List<ClothesData> clothesToBuy = new List<ClothesData>();
        private List<ClothesData> clothesInBasket = new List<ClothesData>();

        public ClothesHelper(Manager manager, string baseURL) : base(manager)
        {
            this.baseURL = baseURL;
        }

        private void GoToURL(string sex, string typeOfClothes)
        {
            switch (sex.ToLower())
            {
                case "male":
                    {
                        switch (typeOfClothes.ToLower())
                        {
                            case "tops":
                                {
                                    driver.Navigate().GoToUrl(baseURL + "/men/tops-men.html");
                                    new WebDriverWait(driver, TimeSpan.FromSeconds(15)).Until(element => element.FindElement(By.CssSelector("span[class='base'][data-ui-id='page-title-wrapper']")).Text == "Tops");
                                    break;
                                }
                            case "bottoms":
                                {
                                    driver.Navigate().GoToUrl(baseURL + "/men/bottoms-men.html");
                                    new WebDriverWait(driver, TimeSpan.FromSeconds(15)).Until(element => element.FindElement(By.CssSelector("span[class='base'][data-ui-id='page-title-wrapper']")).Text == "Bottoms");
                                    break;
                                }
                        }
                        break;
                    }
                case "female":
                    {
                        switch (typeOfClothes.ToLower())
                        {
                            case "tops":
                                {
                                    driver.Navigate().GoToUrl(baseURL + "/women/tops-women.html");
                                    new WebDriverWait(driver, TimeSpan.FromSeconds(15)).Until(element => element.FindElement(By.CssSelector("span[class='base'][data-ui-id='page-title-wrapper']")).Text == "Tops");
                                    break;
                                }
                            case "bottoms":
                                {
                                    driver.Navigate().GoToUrl(baseURL + "/women/bottoms-women.html");
                                    new WebDriverWait(driver, TimeSpan.FromSeconds(15)).Until(element => element.FindElement(By.CssSelector("span[class='base'][data-ui-id='page-title-wrapper']")).Text == "Bottoms");
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

        public void BuyClothes(string sex, string typeOfClothes, string option)
        {
            ClothesData clothes = new ClothesData();
            if (option.ToLower() == "category")
            {
                clothes = GetObjectWithCategory(sex, typeOfClothes);
            }
            if (option.ToLower() == "style")
            {
                clothes = GetObjectWithStyle(sex, typeOfClothes);
            }
            if (option.ToLower() == "size")
            {
                clothes = GetObjectWithSize(sex, typeOfClothes);
            }
            if (option.ToLower() == "price")
            {
                clothes = GetObjectWithPrice(sex, typeOfClothes);
            }
            if (option.ToLower() == "color")
            {
                clothes = GetObjectWithColor(sex, typeOfClothes);
            }
            if (option.ToLower() == "material")
            {
                clothes = GetObjectWithMaterial(sex, typeOfClothes);
            }
            if (option.ToLower() == "eco collection")
            {
                clothes = GetObjectWithEcoCollection(sex, typeOfClothes);
            }
            if (option.ToLower() == "performance fabric")
            {
                clothes = GetObjectWithPerformanceFabric(sex, typeOfClothes);
            }
            if (option.ToLower() == "erin recommends")
            {
                clothes = GetObjectWithErinRecommends(sex, typeOfClothes);
            }
            if (option.ToLower() == "new")
            {
                clothes = GetObjectWithNew(sex, typeOfClothes);
            }
            if (option.ToLower() == "sale")
            {
                clothes = GetObjectWithSale(sex, typeOfClothes);
            }
            if (option.ToLower() == "pattern")
            {
                clothes = GetObjectWithPattern(sex, typeOfClothes);
            }
            if (option.ToLower() == "climate")
            {
                clothes = GetObjectWithClimate(sex, typeOfClothes);
            }
            ChooseSingleOption(clothes, sex, typeOfClothes, option);
            AddClothesToCart(clothes, sex, typeOfClothes);
            CheckProductsInCartPage(sex, typeOfClothes);
            ConfirmThePurchase();
        }

        private void ConfirmThePurchase()
        {
            Thread.Sleep(5000);
            driver.FindElement(By.CssSelector("button[class='action primary checkout'][data-role='proceed-to-checkout']")).Click();
            Thread.Sleep(15000);
            driver.FindElement(By.ClassName("radio")).Click();
            driver.FindElement(By.CssSelector("button[data-role='opc-continue']")).Click();
            Thread.Sleep(15000);
            driver.FindElement(By.CssSelector("button[class='action primary checkout']")).Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(30)).Until(element => element.FindElement(By.ClassName("base")).Text == "Thank you for your purchase!");
        }

        private void CheckProductsInCartPage(string sex, string typeOfClothes)
        {
            manager.Navigator.GoToShoppingCartPage();
            IWebElement cartTable = driver.FindElement(By.Id("shopping-cart-table"));
            List<IWebElement> products = cartTable.FindElements(By.CssSelector("tbody[class='cart item']")).ToList();
            foreach (IWebElement product in products)
            {
                IWebElement itemDetailes = product.FindElement(By.ClassName("item-info"));
                IWebElement itemOptions = itemDetailes.FindElement(By.CssSelector("td[data-th='Item']")).FindElement(By.ClassName("product-item-details"));
                string productName = itemDetailes.FindElement(By.ClassName("product-item-name")).FindElement(By.TagName("a")).Text;
                List<IWebElement> itemInfo = itemOptions.FindElement(By.ClassName("item-options")).FindElements(By.TagName("dd")).ToList();
                string productSize = itemInfo[0].Text;
                string productColor = itemInfo[1].Text;
                string productPrice = itemDetailes.FindElement(By.ClassName("price")).Text.Replace("$","");
                GetListOfClothesFromBasket(sex, typeOfClothes, productSize, productColor, productName, productPrice);
            }
            clothesToBuy.Sort();
            clothesInBasket.Sort();
            clothesToBuy.Equals(clothesInBasket);
        }

        private void GetListOfClothesFromBasket(string sex, string typeOfClothes, string productSize, string productColor, string productName, string productPrice)
        {
            if (sex.ToLower() == "female")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    ClothesData clothes = new ClothesData()
                    {
                        WomenClothes = new WomenClothes()
                        {
                            TopsWoman = new TopsWoman()
                        }
                    };
                    MakeListOfClothes(clothes, sex, typeOfClothes, productSize, productColor, productName, productPrice);
                }
                if (typeOfClothes.ToLower() == "bottoms")
                {
                    ClothesData clothes = new ClothesData()
                    {
                        WomenClothes = new WomenClothes()
                        {
                            BottomsWoman = new BottomsWoman()
                        }
                    };
                    MakeListOfClothes(clothes, sex, typeOfClothes, productSize, productColor, productName, productPrice);
                }
            }
            else
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    ClothesData clothes = new ClothesData()
                    {
                        MenClothes = new MenClothes()
                        {
                            TopsMan = new TopsMan()
                        }
                    };
                    MakeListOfClothes(clothes, sex, typeOfClothes, productSize, productColor, productName, productPrice);
                }
                if (typeOfClothes.ToLower() == "bottoms")
                {
                    ClothesData clothes = new ClothesData()
                    {
                        MenClothes = new MenClothes()
                        {
                            BottomsMan = new BottomsMan()
                        }
                    };
                    MakeListOfClothes(clothes, sex, typeOfClothes, productSize, productColor, productName, productPrice);
                }
            }
        }

        private ClothesData GetObjectWithCategory(string sex, string typeOfClothes)
        {
            ClothesData clothes;
            if (sex.ToLower() == "female")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    clothes = new ClothesData()
                    {
                        WomenClothes = new WomenClothes
                        {
                            TopsWoman = new TopsWoman
                            {
                                CategoryTops = new CategoryTops()
                                {
                                    Category = GetCategoryTops(sex)
                                }
                            }
                        }
                    };
                }
                else
                {
                    clothes = new ClothesData()
                    {
                        WomenClothes = new WomenClothes
                        {
                            BottomsWoman = new BottomsWoman
                            {
                                CategoryBottoms = new CategoryBottoms()
                                {
                                    Category = GetCategoryBottoms(sex)
                                }
                            }
                        }
                    };
                }
            }
            else
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    clothes = new ClothesData()
                    {
                        MenClothes = new MenClothes
                        {
                            TopsMan = new TopsMan
                            {
                                CategoryTops = new CategoryTops()
                                {
                                    Category = GetCategoryTops(sex)
                                }
                            }
                        }
                    };
                }
                else
                {
                    clothes = new ClothesData()
                    {
                        MenClothes = new MenClothes
                        {
                            BottomsMan = new BottomsMan
                            {
                                CategoryBottoms = new CategoryBottoms()
                                {
                                    Category = GetCategoryBottoms(sex)
                                }
                            }
                        }
                    };
                }
            }
            return clothes;
        }

        private ClothesData GetObjectWithStyle(string sex, string typeOfClothes)
        {
            ClothesData clothes;
            if (sex.ToLower() == "female")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    clothes = new ClothesData()
                    {
                        WomenClothes = new WomenClothes
                        {
                            TopsWoman = new TopsWoman
                            {
                                StyleTops = new StyleTops()
                                {
                                    Style = GetStyleTops(sex)
                                }
                            }
                        }
                    };
                }
                else
                {
                    clothes = new ClothesData()
                    {
                        WomenClothes = new WomenClothes
                        {
                            BottomsWoman = new BottomsWoman
                            {
                                StyleBottoms = new StyleBottoms()
                                {
                                    Style = GetStyleBottoms(sex)
                                }
                            }
                        }
                    };
                }
            }
            else
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    clothes = new ClothesData()
                    {
                        MenClothes = new MenClothes
                        {
                            TopsMan = new TopsMan
                            {
                                StyleTops = new StyleTops()
                                {
                                    Style = GetStyleTops(sex)
                                }
                            }
                        }
                    };
                }
                else
                {
                    clothes = new ClothesData()
                    {
                        MenClothes = new MenClothes
                        {
                            BottomsMan = new BottomsMan
                            {
                                StyleBottoms = new StyleBottoms()
                                {
                                    Style = GetStyleBottoms(sex)
                                }
                            }
                        }
                    };
                }
            }
            return clothes;
        }

        private ClothesData GetObjectWithSize(string sex, string typeOfClothes)
        {
            ClothesData clothes;
            if (sex.ToLower() == "female")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    clothes = new ClothesData()
                    {
                        WomenClothes = new WomenClothes
                        {
                            TopsWoman = new TopsWoman
                            {
                                SizeTops = new SizeTops()
                                {
                                    Size = GetSizeTops(sex)
                                }
                            }
                        }
                    };
                }
                else
                {
                    clothes = new ClothesData()
                    {
                        WomenClothes = new WomenClothes
                        {
                            BottomsWoman = new BottomsWoman
                            {
                                SizeBottoms = new SizeBottoms()
                                {
                                    Size = GetSizeBottoms(sex)
                                }
                            }
                        }
                    };
                }
            }
            else
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    clothes = new ClothesData()
                    {
                        MenClothes = new MenClothes
                        {
                            TopsMan = new TopsMan
                            {
                                SizeTops = new SizeTops()
                                {
                                    Size = GetSizeTops(sex)
                                }
                            }
                        }
                    };
                }
                else
                {
                    clothes = new ClothesData()
                    {
                        MenClothes = new MenClothes
                        {
                            BottomsMan = new BottomsMan
                            {
                                SizeBottoms = new SizeBottoms()
                                {
                                    Size = GetSizeBottoms(sex)
                                }
                            }
                        }
                    };
                }
            }
            return clothes;
        }

        private ClothesData GetObjectWithPrice(string sex, string typeOfClothes)
        {
            ClothesData clothes;
            if (sex.ToLower() == "female")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    clothes = new ClothesData()
                    {
                        WomenClothes = new WomenClothes
                        {
                            TopsWoman = new TopsWoman
                            {
                                PriceTops = new PriceTops()
                                {
                                    Price = GetPriceTops(sex)
                                }
                            }
                        }
                    };
                }
                else
                {
                    clothes = new ClothesData()
                    {
                        WomenClothes = new WomenClothes
                        {
                            BottomsWoman = new BottomsWoman
                            {
                                PriceBottoms = new PriceBottoms()
                                {
                                    Price = GetPriceBottoms(sex)
                                }
                            }
                        }
                    };
                }
            }
            else
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    clothes = new ClothesData()
                    {
                        MenClothes = new MenClothes
                        {
                            TopsMan = new TopsMan
                            {
                                PriceTops = new PriceTops()
                                {
                                    Price = GetPriceTops(sex)
                                }
                            }
                        }
                    };
                }
                else
                {
                    clothes = new ClothesData()
                    {
                        MenClothes = new MenClothes
                        {
                            BottomsMan = new BottomsMan
                            {
                                PriceBottoms = new PriceBottoms()
                                {
                                    Price = GetPriceBottoms(sex)
                                }
                            }
                        }
                    };
                }
            }
            return clothes;
        }

        private ClothesData GetObjectWithColor(string sex, string typeOfClothes)
        {
            ClothesData clothes;
            if (sex.ToLower() == "female")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    clothes = new ClothesData()
                    {
                        WomenClothes = new WomenClothes
                        {
                            TopsWoman = new TopsWoman
                            {
                                ColorTops = new ColorTops()
                                {
                                    Color = GetColorTops(sex)
                                }
                            }
                        }
                    };
                }
                else
                {
                    clothes = new ClothesData()
                    {
                        WomenClothes = new WomenClothes
                        {
                            BottomsWoman = new BottomsWoman
                            {
                                ColorBottoms = new ColorBottoms()
                                {
                                    Color = GetColorBottoms(sex)
                                }
                            }
                        }
                    };
                }
            }
            else
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    clothes = new ClothesData()
                    {
                        MenClothes = new MenClothes
                        {
                            TopsMan = new TopsMan
                            {
                                ColorTops = new ColorTops()
                                {
                                    Color = GetColorTops(sex)
                                }
                            }
                        }
                    };
                }
                else
                {
                    clothes = new ClothesData()
                    {
                        MenClothes = new MenClothes
                        {
                            BottomsMan = new BottomsMan
                            {
                                ColorBottoms = new ColorBottoms()
                                {
                                    Color = GetColorBottoms(sex)
                                }
                            }
                        }
                    };
                }
            }
            return clothes;
        }

        private ClothesData GetObjectWithMaterial(string sex, string typeOfClothes)
        {
            ClothesData clothes;
            if (sex.ToLower() == "female")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    clothes = new ClothesData()
                    {
                        WomenClothes = new WomenClothes
                        {
                            TopsWoman = new TopsWoman
                            {
                                MaterialTops = new MaterialTops()
                                {
                                    Material = GetMaterialTops(sex)
                                }
                            }
                        }
                    };
                }
                else
                {
                    clothes = new ClothesData()
                    {
                        WomenClothes = new WomenClothes
                        {
                            BottomsWoman = new BottomsWoman
                            {
                                MaterialBottoms = new MaterialBottoms()
                                {
                                    Material = GetMaterialBottoms(sex)
                                }
                            }
                        }
                    };
                }
            }
            else
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    clothes = new ClothesData()
                    {
                        MenClothes = new MenClothes
                        {
                            TopsMan = new TopsMan
                            {
                                MaterialTops = new MaterialTops()
                                {
                                    Material = GetMaterialTops(sex)
                                }
                            }
                        }
                    };
                }
                else
                {
                    clothes = new ClothesData()
                    {
                        MenClothes = new MenClothes
                        {
                            BottomsMan = new BottomsMan
                            {
                                MaterialBottoms = new MaterialBottoms()
                                {
                                    Material = GetMaterialBottoms(sex)
                                }
                            }
                        }
                    };
                }
            }
            return clothes;
        }

        private ClothesData GetObjectWithEcoCollection(string sex, string typeOfClothes)
        {
            ClothesData clothes;
            if (sex.ToLower() == "female")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    clothes = new ClothesData()
                    {
                        WomenClothes = new WomenClothes
                        {
                            TopsWoman = new TopsWoman
                            {
                                EcoCollection = GetYesOrNo()
                            }
                        }
                    };
                }
                else
                {
                    clothes = new ClothesData()
                    {
                        WomenClothes = new WomenClothes
                        {
                            BottomsWoman = new BottomsWoman
                            {
                                EcoCollection = GetYesOrNo()
                            }
                        }
                    };
                }
            }
            else
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    clothes = new ClothesData()
                    {
                        MenClothes = new MenClothes
                        {
                            TopsMan = new TopsMan
                            {
                                EcoCollection = GetYesOrNo()
                            }
                        }
                    };
                }
                else
                {
                    clothes = new ClothesData()
                    {
                        MenClothes = new MenClothes
                        {
                            BottomsMan = new BottomsMan
                            {
                                EcoCollection = GetYesOrNo()
                            }
                        }
                    };
                }
            }
            return clothes;
        }

        private ClothesData GetObjectWithPerformanceFabric(string sex, string typeOfClothes)
        {
            ClothesData clothes;
            if (sex.ToLower() == "female")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    clothes = new ClothesData()
                    {
                        WomenClothes = new WomenClothes
                        {
                            TopsWoman = new TopsWoman
                            {
                                PerformanceFabric = GetYesOrNo()
                            }
                        }
                    };
                }
                else
                {
                    clothes = new ClothesData()
                    {
                        WomenClothes = new WomenClothes
                        {
                            BottomsWoman = new BottomsWoman
                            {
                                PerformanceFabric = GetYesOrNo()
                            }
                        }
                    };
                }
            }
            else
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    clothes = new ClothesData()
                    {
                        MenClothes = new MenClothes
                        {
                            TopsMan = new TopsMan
                            {
                                PerformanceFabric = GetYesOrNo()
                            }
                        }
                    };
                }
                else
                {
                    clothes = new ClothesData()
                    {
                        MenClothes = new MenClothes
                        {
                            BottomsMan = new BottomsMan
                            {
                                PerformanceFabric = GetYesOrNo()
                            }
                        }
                    };
                }
            }
            return clothes;
        }

        private ClothesData GetObjectWithErinRecommends(string sex, string typeOfClothes)
        {
            ClothesData clothes;
            if (sex.ToLower() == "female")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    clothes = new ClothesData()
                    {
                        WomenClothes = new WomenClothes
                        {
                            TopsWoman = new TopsWoman
                            {
                                ErinRecommends = GetYesOrNo()
                            }
                        }
                    };
                }
                else
                {
                    clothes = new ClothesData()
                    {
                        WomenClothes = new WomenClothes
                        {
                            BottomsWoman = new BottomsWoman
                            {
                                ErinRecommends = GetYesOrNo()
                            }
                        }
                    };
                }
            }
            else
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    clothes = new ClothesData()
                    {
                        MenClothes = new MenClothes
                        {
                            TopsMan = new TopsMan
                            {
                                ErinRecommends = GetYesOrNo()
                            }
                        }
                    };
                }
                else
                {
                    clothes = new ClothesData()
                    {
                        MenClothes = new MenClothes
                        {
                            BottomsMan = new BottomsMan
                            {
                                ErinRecommends = GetYesOrNo()
                            }
                        }
                    };
                }
            }
            return clothes;
        }

        private ClothesData GetObjectWithNew(string sex, string typeOfClothes)
        {
            ClothesData clothes;
            if (sex.ToLower() == "female")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    clothes = new ClothesData()
                    {
                        WomenClothes = new WomenClothes
                        {
                            TopsWoman = new TopsWoman
                            {
                                New = GetYesOrNo()
                            }
                        }
                    };
                }
                else
                {
                    clothes = new ClothesData()
                    {
                        WomenClothes = new WomenClothes
                        {
                            BottomsWoman = new BottomsWoman
                            {
                                New = GetYesOrNo()
                            }
                        }
                    };
                }
            }
            else
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    clothes = new ClothesData()
                    {
                        MenClothes = new MenClothes
                        {
                            TopsMan = new TopsMan
                            {
                                New = GetYesOrNo()
                            }
                        }
                    };
                }
                else
                {
                    clothes = new ClothesData()
                    {
                        MenClothes = new MenClothes
                        {
                            BottomsMan = new BottomsMan
                            {
                                New = GetYesOrNo()
                            }
                        }
                    };
                }
            }
            return clothes;
        }

        private ClothesData GetObjectWithSale(string sex, string typeOfClothes)
        {
            ClothesData clothes;
            if (sex.ToLower() == "female")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    clothes = new ClothesData()
                    {
                        WomenClothes = new WomenClothes
                        {
                            TopsWoman = new TopsWoman
                            {
                                Sale = GetYesOrNo()
                            }
                        }
                    };
                }
                else
                {
                    clothes = new ClothesData()
                    {
                        WomenClothes = new WomenClothes
                        {
                            BottomsWoman = new BottomsWoman
                            {
                                Sale = GetYesOrNo()
                            }
                        }
                    };
                }
            }
            else
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    clothes = new ClothesData()
                    {
                        MenClothes = new MenClothes
                        {
                            TopsMan = new TopsMan
                            {
                                Sale = GetYesOrNo()
                            }
                        }
                    };
                }
                else
                {
                    clothes = new ClothesData()
                    {
                        MenClothes = new MenClothes
                        {
                            BottomsMan = new BottomsMan
                            {
                                Sale = GetYesOrNo()
                            }
                        }
                    };
                }
            }
            return clothes;
        }

        private ClothesData GetObjectWithPattern(string sex, string typeOfClothes)
        {
            ClothesData clothes;
            if (sex.ToLower() == "female")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    clothes = new ClothesData()
                    {
                        WomenClothes = new WomenClothes
                        {
                            TopsWoman = new TopsWoman
                            {
                                PatternTops = new PatternTops()
                                {
                                    Pattern = GetPatternTops(sex)
                                }
                            }
                        }
                    };
                }
                else
                {
                    clothes = new ClothesData()
                    {
                        WomenClothes = new WomenClothes
                        {
                            BottomsWoman = new BottomsWoman
                            {
                                PatternBottoms = new PatternBottoms()
                                {
                                    Pattern = GetPatternBottoms(sex)
                                }
                            }
                        }
                    };
                }
            }
            else
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    clothes = new ClothesData()
                    {
                        MenClothes = new MenClothes
                        {
                            TopsMan = new TopsMan
                            {
                                PatternTops = new PatternTops()
                                {
                                    Pattern = GetPatternTops(sex)
                                }
                            }
                        }
                    };
                }
                else
                {
                    clothes = new ClothesData()
                    {
                        MenClothes = new MenClothes
                        {
                            BottomsMan = new BottomsMan
                            {
                                PatternBottoms = new PatternBottoms()
                                {
                                    Pattern = GetPatternBottoms(sex)
                                }
                            }
                        }
                    };
                }
            }
            return clothes;
        }

        private ClothesData GetObjectWithClimate(string sex, string typeOfClothes)
        {
            ClothesData clothes;
            if (sex.ToLower() == "female")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    clothes = new ClothesData()
                    {
                        WomenClothes = new WomenClothes
                        {
                            TopsWoman = new TopsWoman
                            {
                                ClimateTops = new ClimateTops()
                                {
                                    Climate = GetClimateTops(sex)
                                }
                            }
                        }
                    };
                }
                else
                {
                    clothes = new ClothesData()
                    {
                        WomenClothes = new WomenClothes
                        {
                            BottomsWoman = new BottomsWoman
                            {
                                ClimateBottoms = new ClimateBottoms()
                                {
                                    Climate = GetClimateBottoms(sex)
                                }
                            }
                        }
                    };
                }
            }
            else
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    clothes = new ClothesData()
                    {
                        MenClothes = new MenClothes
                        {
                            TopsMan = new TopsMan
                            {
                                ClimateTops = new ClimateTops()
                                {
                                    Climate = GetClimateTops(sex)
                                }
                            }
                        }
                    };
                }
                else
                {
                    clothes = new ClothesData()
                    {
                        MenClothes = new MenClothes
                        {
                            BottomsMan = new BottomsMan
                            {
                                ClimateBottoms = new ClimateBottoms()
                                {
                                    Climate = GetClimateBottoms(sex)
                                }
                            }
                        }
                    };
                }
            }
            return clothes;
        }

        private void ChooseSingleOption(ClothesData clothes, string sex, string typeOfClothes, string option)
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
            int countOfClothes = GetCountOfClothes();
            int countOfClothesFromToolbar = GetCountOfItemsFromToolbar();
            Assert.AreEqual(countOfClothes, countOfClothesFromToolbar);
        }

        // get count of clothes after choosing some shopping option
        private int GetCountOfClothes()
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
                    Thread.Sleep(5000);
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
        private int GetCountOfItemsFromToolbar()
        {
            if (driver.FindElement(By.Id("toolbar-amount")).Text.Contains("of"))
            {
                return Int32.Parse(driver.FindElement(By.Id("toolbar-amount")).Text.Split(new string[] { "of" }, StringSplitOptions.None)[1]);
            }
            else
            {
                return Int32.Parse(driver.FindElement(By.Id("toolbar-amount")).FindElement(By.ClassName("toolbar-number")).Text);
            }
        }

        private void AddClothesToCart(ClothesData clothes, string sex, string typeOfClothes)
        {
            IWebElement productsGrid = driver.FindElement(By.CssSelector("div[class='products wrapper grid products-grid']")).FindElement(By.CssSelector("ol[class='products list items product-items']"));
            List<IWebElement> listOfProducts = productsGrid.FindElements(By.CssSelector("li[class='item product product-item']")).ToList();
            IWebElement product = listOfProducts[rnd.Next(listOfProducts.Count())].FindElement(By.ClassName("product-item-info")); // get product info like sizes and colors  
            string choosenSize;
            string choosenColor;
            // check if size is already choosen
            try
            {
                choosenSize = product.FindElement(By.CssSelector("div[class='swatch-attribute size']")).FindElement(By.CssSelector("div[class='swatch-option text selected']")).Text; 
            }
            catch
            {
                choosenSize = null;
            }

            // check if color is already choosen
            try
            {
                choosenColor = product.FindElement(By.CssSelector("div[class='swatch-attribute color']")).FindElement(By.CssSelector("div[class='swatch-option color selected']")).GetAttribute("option-label"); 
            }
            catch
            {
                choosenColor = null;
            }

            // if size is not choosen, so choose one
            if (choosenSize == null)
            {
                product.FindElement(By.CssSelector("div[class='swatch-attribute size']")).FindElement(By.CssSelector("div[class='swatch-option text']")).Click();
                choosenSize = product.FindElement(By.CssSelector("div[class='swatch-attribute size']")).FindElement(By.CssSelector("div[class='swatch-option text selected']")).Text;
            }
            // if color is not choosen, so choose one
            if (choosenColor == null)
            {
                product.FindElement(By.CssSelector("div[class='swatch-attribute color']")).FindElement(By.CssSelector("div[class='swatch-option color']")).Click();
                choosenColor = product.FindElement(By.CssSelector("div[class='swatch-attribute color']")).FindElement(By.CssSelector("div[class='swatch-option color selected']")).GetAttribute("option-label");
            }
            string productName = product.FindElement(By.CssSelector("strong[class='product name product-item-name']")).FindElement(By.TagName("a")).Text;
            string productPrice = product.FindElement(By.ClassName("price")).Text.Replace("$","");
            MakeListOfClothes(clothes, sex, typeOfClothes, choosenSize, choosenColor, productName, productPrice);
            product.FindElement(By.CssSelector("button[type='submit'][title='Add to Cart']")).Click();
            Thread.Sleep(10000);
        }

        private void MakeListOfClothes(ClothesData clothes, string sex, string typeOfClothes, string choosenSize, string choosenColor, string productName, string productPrice)
        {
            if (sex.ToLower() == "female")
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    clothes.WomenClothes.TopsWoman.Name = productName;
                    clothes.WomenClothes.TopsWoman.PriceTops = new PriceTops
                    {
                        Price = productPrice
                    };
                    clothes.WomenClothes.TopsWoman.ColorTops = new ColorTops
                    {
                        Color = choosenColor
                    };
                    clothes.WomenClothes.TopsWoman.SizeTops = new SizeTops
                    {
                        Size = choosenSize
                    };
                }
                if (typeOfClothes.ToLower() == "bottoms")
                {
                    clothes.WomenClothes.BottomsWoman.Name = productName;
                    clothes.WomenClothes.BottomsWoman.PriceBottoms = new PriceBottoms
                    {
                        Price = productPrice
                    };
                    clothes.WomenClothes.BottomsWoman.ColorBottoms = new ColorBottoms
                    {
                        Color = choosenColor
                    };
                    clothes.WomenClothes.BottomsWoman.SizeBottoms = new SizeBottoms
                    {
                        Size = choosenSize
                    };
                }
            }
            else
            {
                if (typeOfClothes.ToLower() == "tops")
                {
                    clothes.MenClothes.TopsMan.Name = productName;
                    clothes.MenClothes.TopsMan.PriceTops = new PriceTops
                    {
                        Price = productPrice
                    };
                    clothes.MenClothes.TopsMan.ColorTops = new ColorTops
                    {
                        Color = choosenColor
                    };
                    clothes.MenClothes.TopsMan.SizeTops = new SizeTops
                    {
                        Size = choosenSize
                    };
                }
                if (typeOfClothes.ToLower() == "bottoms")
                {
                    clothes.MenClothes.BottomsMan.Name = productName;
                    clothes.MenClothes.BottomsMan.PriceBottoms = new PriceBottoms
                    {
                        Price = productPrice
                    };
                    clothes.MenClothes.BottomsMan.ColorBottoms = new ColorBottoms
                    {
                        Color = choosenColor
                    };
                    clothes.MenClothes.BottomsMan.SizeBottoms = new SizeBottoms
                    {
                        Size = choosenSize
                    };
                }
            }
            if (clothesToBuy.Count() == 0)
            {
                clothesToBuy.Add(clothes);
            }
            else
            {
                clothesInBasket.Add(clothes);
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
            if (option.ToLower() == "color")
            {
                ClickColor(option, valueFromOption);
            }
            else if (option.ToLower() == "size")
            {
                ClickSize(option, valueFromOption);
            }
            else
            {
                IWebElement itemsElement = GetShoppingOptions().Where(element => element.FindElement(By.ClassName("filter-options-title")).Text.ToLower() == option.ToLower()).First().FindElement(By.CssSelector("ol[class='items']")); // list of items with links for each category
                if (option.ToLower() == "price")
                {
                    ClickPrice(option, valueFromOption, itemsElement);
                }
                else
                {
                    itemsElement.FindElements(By.CssSelector("li[class='item']")).Where(element => Regex.Split(element.FindElement(By.TagName("a")).Text.ToLower(), @"\d")[0].Trim() == valueFromOption).First().FindElement(By.TagName("a")).Click(); // click item with link for choosen category
                }
            }
            // wait
            Thread.Sleep(5000);
        }

        private void ClickPrice(string option, string valueFromOption, IWebElement itemsElement)
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

        private void ClickColor(string option, string valueFromOption)
        {
            IWebElement itemsElement = GetShoppingOptions().Where(element => element.FindElement(By.ClassName("filter-options-title")).Text.ToLower() == option.ToLower()).First().FindElement(By.CssSelector("div[class='swatch-attribute swatch-layered color']")); // list of colors

            ICollection<IWebElement> webElementsColors = itemsElement.FindElement(By.CssSelector("div[class='swatch-attribute-options clearfix']")).FindElements(By.TagName("a")); // list of items with links

            webElementsColors.Where(element => element.FindElement(By.TagName("div")).GetAttribute("option-label").ToLower() == valueFromOption.ToLower()).First().FindElement(By.TagName("div")).Click(); // click choosen color
        }

        private void ClickSize(string option, string kindOfOptions)
        {
            IWebElement itemsElement = GetShoppingOptions().Where(element => element.FindElement(By.ClassName("filter-options-title")).Text.ToLower() == option.ToLower()).First().FindElement(By.CssSelector("div[class='swatch-attribute-options clearfix']")); // list of items with links for each category
            
            itemsElement.FindElements(By.TagName("a")).Where(element => element.GetAttribute("aria-label").ToLower().Trim() == kindOfOptions).First().FindElement(By.TagName("div")).Click();  // click item with link for choosen category
        }
    }
}
