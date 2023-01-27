using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace AutotestingOnlineShops.Luma
{
    [TestFixture]
    public class ShoppingTests : AuthTestBase
    {
        [Test]
        public void BuyManClothes()
        {

        }

        [Test]
        public void BuyWomanClothes()
        {
            string sex = "Female";
            ClothesData clothes = new ClothesData()
            {
                WomenClothes = new WomenClothes
                {
                    TopsWoman = new TopsWoman
                    {
                        CategoryTops = new CategoryTops()
                        {
                            Category = GetCategoryTops(sex)
                        }

                        //StyleTops = new StyleTops()
                        //{
                        //    Style = GetStyleTops(sex)
                        //}

                        //SizeTops = new SizeTops()
                        //{
                        //    Size = GetSize(sex)
                        //}

                        //PriceTops = new PriceTops
                        //{
                        //    Price = GetPrice(sex)
                        //}

                        //ColorTops = new ColorTops()
                        //{
                        //    Color = GetColor(sex)
                        //}

                        //MaterialTops = new MaterialTops()
                        //{
                        //    Material = GetMaterial(sex)
                        //}

                        //EcoCollection = "no"

                        //PerformanceFabric = "yes" 

                        //ErinRecommends = "no"

                        //New = "yes"

                        //Sale = "no"

                        //PatternTops = new PatternTops()
                        //{
                        //    Pattern = GetPattern(sex)
                        //}

                        //ClimateTops = new ClimateTops()
                        //{
                        //    Climate = GetClimate(sex)
                        //}
                    }
                }
            };
            app.Clothes.ChooseSingleOption(clothes, sex, "tops", "category");
            int countOfClothes = app.Clothes.GetCountOfClothes();
            int countOfClothesFromToolbar =  app.Clothes.GetCountOfItemsFromToolbar();
            Assert.AreEqual(countOfClothes, countOfClothesFromToolbar);
        }

        [Test]
        public void BuyGear()
        {

        }
    }
}
