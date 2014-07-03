namespace Implementation.Models
{
    using Algorithms;
    using System.Collections.Generic;
    using System.Linq;

    public class Package : Asset
    {
        public Package(string name, List<Asset> assets)
            : this(name, 0, assets)
        {

        }
        public Package(string name, decimal price = 0, List<Asset> contents = null)
            : base(name, price)
        {
            Contents = contents ?? new List<Asset>();
        }

        internal void AddAsset(Asset asset)
        {
            Contents.Add(asset);
        }

        public List<Asset> Contents { get; private set; }

        public IEnumerable<Asset> GetAllAssets()
        {
            return Contents
                .Where(item => item.GetType() == typeof(Asset))
                .Union(Contents.OfType<Package>().SelectMany(package => package.GetAllAssets()));
        }

        public decimal CalculateTotalPrice()
        {
            Price = PriceCalculator.CalculatePackagePrice(this);
            return Price;
        }
    }
}