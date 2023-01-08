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
            GetCategoryTops("man");
            //ClothesData clothes = new ClothesData()
            //{
            //    MenClothes = new MenClothes
            //    {
            //        TopsMan = new TopsMan
            //        {
            //            CategoryTops = new CategoryTops()
            //            {
            //                Category = GetCategoryTops("man")
            //            },

            //            Style = new StyleTops()
            //            {
            //                Style = GetStyleTops("man")
            //            }
            //        }
                    //BottomsMan = new BottomsMan()
                    //{
                    //    Category = new Category()
                    //    {
                    //        Category2 = GetCategoryTops("Bottoms").Category2
                    //    }
                    //}
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
            //    }
            //};
        }

        [Test]
        public void BuyGear()
        {

        }
    }
}
