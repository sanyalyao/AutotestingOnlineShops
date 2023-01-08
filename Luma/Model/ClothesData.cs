namespace AutotestingOnlineShops.Luma
{
    public class ClothesData
    {
        public MenClothes MenClothes { get; set; }

        public WomenClothes WomenClothes { get; set; }
    }

    public class MenClothes
    {
        public TopsMan TopsMan { get; set; }

        //public BottomsMan BottomsMan { get; set; }
    }
    public class WomenClothes
    {
        public TopsMan TopsWoman { get; set; }

        //public BottomsWoman BottomsWoman { get; set; }
    }

    public class TopsMan
    {
        public CategoryTops CategoryTops { get; set; }

        public StyleTops Style { get; set; }
        public SizeTops Size { get; set; }
        //public string Price { get; set; }
        //public string Color { get; set; }
        //public string Material { get; set; }
        //public string EcoCollection { get; set; }
        //public string PerfomanceFabric { get; set; }
        //public string ErinRecommends { get; set; }
        //public string New { get; set; }
        //public string Sale { get; set; }
        //public string Pattern { get; set; }
        //public string Climate { get; set; }
    }

    public class TopsWoman
    {
        public CategoryTops CategoryTops { get; set; }

        public StyleTops Style { get; set; }
        public SizeTops Size { get; set; }
        //public string Price { get; set; }
        //public string Color { get; set; }
        //public string Material { get; set; }
        //public string EcoCollection { get; set; }
        //public string PerfomanceFabric { get; set; }
        //public string ErinRecommends { get; set; }
        //public string New { get; set; }
        //public string Sale { get; set; }
        //public string Pattern { get; set; }
        //public string Climate { get; set; }
    }

    //public class BottomsMan
    //{
    //    [XmlElement(ElementName = "Category")]
    //    public Category Category { get; set; }
    //}

    //[XmlRoot(ElementName = "BottomsWoman")]
    //public class BottomsWoman
    //{
    //    [XmlElement(ElementName = "Category")]
    //    public Category Category { get; set; }
    //}

    public class CategoryTops
    {
        public string Category { get; set; }

    }

    public class StyleTops
    {
        public string Style { get; set; }

    }

    public class SizeTops
    {
        public string Size { get; set; }
    }
}
