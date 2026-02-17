namespace OpsMax.Models
{
    public class Vendor
    {
        public int DCLink { get; set; }       // Must be int
        public string Name { get; set; } = string.Empty;
    }

    public class StkItem
    {
        public int StockLink { get; set; }    // Must be int
        public string Description_1 { get; set; } = string.Empty;
    }
}
