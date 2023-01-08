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
        public TopsWoman TopsWoman { get; set; }

        //public BottomsWoman BottomsWoman { get; set; }
    }

    public class TopsMan
    {
        public CategoryTops CategoryTops { get; set; }

        public StyleTops StyleTops { get; set; }
        public SizeTops SizeTops { get; set; }
        //public string Price { get; set; }
        public ColorTops ColorTops { get; set; }
        public MaterialTops MaterialTops { get; set; }
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

        public StyleTops StyleTops { get; set; }
        public SizeTops SizeTops { get; set; }
        //public string Price { get; set; }
        public ColorTops ColorTops { get; set; }
        public MaterialTops MaterialTops { get; set; }
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

    public class ColorTops
    {
        public string Color { get; set; }
    }

    public class MaterialTops
    {
        public string Material { get; set; }
    }
}
