namespace Implementation.Models
{
    using System.Collections.Generic;

    public class ParticipantBids : Dictionary<Asset, Bid>
    {
        internal ParticipantBids(List<Asset> assets)
        {
            assets.ForEach(asset => Add(asset, null));
        }

        public void BidForAsset(Bid bid, Asset asset)
        {
            if (ContainsKey(asset))
            {
                this[asset] = bid;
            }
            else
            {
                Add(asset, bid);
            }
        }
    }
}