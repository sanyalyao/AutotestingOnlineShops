using NUnit.Framework;

namespace AutotestingOnlineShops.Luma
{
    [TestFixture]
    public class ShoppingTests : AuthTestBase
    {
        [Test]
        public void BuyManBottoms()
        {
            string sex = "Male";
            ClothesData clothes = new ClothesData()
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
            app.Clothes.BuyClothes(clothes, sex, "bottoms", "category");
        }

        [Test]
        public void BuyManTops()
        {
            string sex = "Male";
            ClothesData clothes = new ClothesData()
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
            app.Clothes.BuyClothes(clothes, sex, "tops", "category");
        }

        [Test]
        public void BuyWomanTops()
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
                    }
                }
            };
            app.Clothes.BuyClothes(clothes, sex, "tops", "category");
        }

        [Test]
        public void BuyWomanBottoms()
        {
            string sex = "Female";
            ClothesData clothes = new ClothesData()
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
            app.Clothes.BuyClothes(clothes, sex, "bottoms", "category");
        }

        [Test]
        public void BuyManTopsByStyle()
        {
            string sex = "Male";
            ClothesData clothes = new ClothesData()
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
            app.Clothes.BuyClothes(clothes, sex, "tops", "style");
        }

        [Test]
        public void BuyGear()
        {

        }
    }
}
