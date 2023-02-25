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

        public BottomsMan BottomsMan { get; set; }
    }
    public class WomenClothes
    {
        public TopsWoman TopsWoman { get; set; }

        public BottomsWoman BottomsWoman { get; set; }
    }

    public class TopsMan
    {
        public string Name { get; set; }
        public CategoryTops CategoryTops { get; set; }

        public StyleTops StyleTops { get; set; }
        public SizeTops SizeTops { get; set; }
        public PriceTops PriceTops { get; set; }
        public ColorTops ColorTops { get; set; }
        public MaterialTops MaterialTops { get; set; }
        public string EcoCollection { get; set; }
        public string PerformanceFabric { get; set; }
        public string ErinRecommends { get; set; }
        public string New { get; set; }
        public string Sale { get; set; }
        public PatternTops PatternTops { get; set; }
        public ClimateTops ClimateTops { get; set; }
    }

    public class TopsWoman
    {
        public string Name { get; set; }
        public CategoryTops CategoryTops { get; set; }
        public StyleTops StyleTops { get; set; }
        public SizeTops SizeTops { get; set; }
        public PriceTops PriceTops { get; set; }
        public ColorTops ColorTops { get; set; }
        public MaterialTops MaterialTops { get; set; }
        public string EcoCollection { get; set; }
        public string PerformanceFabric { get; set; }
        public string ErinRecommends { get; set; }
        public string New { get; set; }
        public string Sale { get; set; }
        public PatternTops PatternTops { get; set; }
        public ClimateTops ClimateTops { get; set; }
    }

    public class BottomsMan
    {
        public string Name { get; set; }
        public CategoryBottoms CategoryBottoms { get; set; }
        public StyleBottoms StyleBottoms { get; set; }
        public SizeBottoms SizeBottoms { get; set; }
        public PriceBottoms PriceBottoms { get; set; }
        public ColorBottoms ColorBottoms { get; set; }
        public MaterialBottoms MaterialBottoms { get; set; }
        public string EcoCollection { get; set; }
        public string PerformanceFabric { get; set; }
        public string ErinRecommends { get; set; }
        public string New { get; set; }
        public string Sale { get; set; }
        public PatternBottoms PatternBottoms { get; set; }
        public ClimateBottoms ClimateBottoms { get; set; }
    }

    public class BottomsWoman
    {
        public string Name { get; set; }
        public CategoryBottoms CategoryBottoms { get; set; }
        public StyleBottoms StyleBottoms { get; set; }
        public SizeBottoms SizeBottoms { get; set; }
        public PriceBottoms PriceBottoms { get; set; }
        public ColorBottoms ColorBottoms { get; set; }
        public MaterialBottoms MaterialBottoms { get; set; }
        public string EcoCollection { get; set; }
        public string PerformanceFabric { get; set; }
        public string ErinRecommends { get; set; }
        public string New { get; set; }
        public string Sale { get; set; }
        public PatternBottoms PatternBottoms { get; set; }
        public ClimateBottoms ClimateBottoms { get; set; }

    }

    public class CategoryTops
    {
        public string Category { get; set; }
    }

    public class CategoryBottoms
    {
        public string Category { get; set; }
    }

    public class StyleTops
    {
        public string Style { get; set; }
    }

    public class StyleBottoms
    {
        public string Style { get; set; }
    }

    public class SizeTops
    {
        public string Size { get; set; }
    }

    public class SizeBottoms
    {
        public string Size { get;set;}
    }

    public class PriceTops
    {
        public string Price { get; set; }
    }

    public class PriceBottoms
    {
        public string Price { get; set; }
    }

    public class ColorTops
    {
        public string Color { get; set; }
    }

    public class ColorBottoms
    {
        public string Color { get; set; }
    }

    public class MaterialTops
    {
        public string Material { get; set; }
    }

    public class MaterialBottoms
    {
        public string Material { get; set; }
    }

    public class PatternTops
    {
        public string Pattern { get; set; }
    }

    public class PatternBottoms
    {
        public string Pattern { get; set; }
    }

    public class ClimateTops
    {
        public string Climate { get; set; }
    }

    public class ClimateBottoms
    {
        public string Climate { get; set; }
    }
}
