using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

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
                        },

                        StyleTops = new StyleTops()
                        {
                            Style = GetStyleTops(sex)
                        },

                        SizeTops = new SizeTops()
                        {
                            Size = GetSize(sex)
                        },

                        ColorTops = new ColorTops()
                        {
                            Color = GetColor(sex)
                        },

                        MaterialTops = new MaterialTops()
                        {
                            Material = GetMaterial(sex)
                        }
                    }
                    //Style = GetStyleClothes("Woman"),
                    //Size = GetSize(),
                    //Price = GetPrice(),
                    //Color = GetColor(),
                    //Material = GetMaterial(),
                    //EcoCollection = GetYesOrNo(),
                    //PerfomanceFabric = GetYesOrNo(),
                    //ErinRecommends = GetYesOrNo(),
                    //Pattern = GetPattern(),
                    //Climate = GetClimate()
                }
            };
        }

        [Test]
        public void BuyGear()
        {

        }
    }
}
