namespace Implementation.Algorithms
{
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    public class PriceCalculator
    {
        public static decimal CalculatePackagePrice(Package package)
        {
            return GetSubPackagePrices(package)
                .Union(GetAssetPrices(package))
                .Sum();
        }

        private static IEnumerable<decimal> GetAssetPrices(Package package)
        {
            return package.Contents.Where(item => item.GetType() == typeof(Asset)).Select(asset => asset.Price);
        }

        private static IEnumerable<decimal> GetSubPackagePrices(Package package)
        {
            return package.Contents.OfType<Package>().Select(item => item.CalculateTotalPrice());
        }
    }
}