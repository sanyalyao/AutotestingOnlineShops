using NUnit.Framework;

namespace AutotestingOnlineShops.Luma
{
    [TestFixture]
    public class ShoppingTests : AuthTestBase
    {

        // category

        [Test]
        public void BuyManBottomsByCategory()
        {
            app.Clothes.BuyClothes("male", "bottoms", "category");
        }

        [Test]
        public void BuyWomanBottomsByCategory()
        {
            app.Clothes.BuyClothes("female", "bottoms", "category");
        }

        [Test]
        public void BuyManTopsByCategory()
        {
            app.Clothes.BuyClothes("male", "tops", "category");
        }

        [Test]
        public void BuyWomanTopsByCategory()
        {
            app.Clothes.BuyClothes("female", "tops", "category");
        }

        // style

        [Test]
        public void BuyManTopsByStyle()
        {
            app.Clothes.BuyClothes("male", "tops", "style");
        }

        [Test]
        public void BuyWomanTopsByStyle()
        {
            app.Clothes.BuyClothes("female", "tops", "style");
        }

        [Test]
        public void BuyManBottomsByStyle()
        {
            app.Clothes.BuyClothes("male", "bottoms", "style");
        }

        [Test]
        public void BuyWomanBottomsByStyle()
        {
            app.Clothes.BuyClothes("female", "bottoms", "style");
        }

        // size

        [Test]
        public void BuyWomanBottomsBySize()
        {
            app.Clothes.BuyClothes("female", "bottoms", "size");
        }

        [Test]
        public void BuyManBottomsBySize()
        {
            app.Clothes.BuyClothes("male", "bottoms", "size");
        }

        [Test]
        public void BuyWomanTopsBySize()
        {
            app.Clothes.BuyClothes("female", "tops", "size");
        }

        [Test]
        public void BuyManTopsBySize()
        {
            app.Clothes.BuyClothes("male", "tops", "size");
        }

        // price

        [Test]
        public void BuyWomanBottomsByPrice()
        {
            app.Clothes.BuyClothes("female", "bottoms", "Price");
        }

        [Test]
        public void BuyManBottomsByPrice()
        {
            app.Clothes.BuyClothes("male", "bottoms", "Price");
        }

        [Test]
        public void BuyWomanTopsByPrice()
        {
            app.Clothes.BuyClothes("female", "tops", "Price");
        }

        [Test]
        public void BuyManTopsByPrice()
        {
            app.Clothes.BuyClothes("male", "tops", "Price");
        }

        // color

        [Test]
        public void BuyWomanBottomsByColor()
        {
            app.Clothes.BuyClothes("female", "bottoms", "color");
        }

        [Test]
        public void BuyManBottomsByColor()
        {
            app.Clothes.BuyClothes("male", "bottoms", "color");
        }

        [Test]
        public void BuyWomanTopsByColor()
        {
            app.Clothes.BuyClothes("female", "tops", "color");
        }

        [Test]
        public void BuyManTopsByColor()
        {
            app.Clothes.BuyClothes("male", "tops", "color");
        }


        public void BuyGear()
        {

        }
    }
}
