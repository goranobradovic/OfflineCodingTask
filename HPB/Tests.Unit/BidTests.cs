using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Unit
{
    using System;
    using Implementation.Models;

    [TestClass]
    public class BidTests
    {
        [TestMethod]
        public void CreateBid()
        {
            var participant = new Participant("John Smith");
            var asset = new Asset("White House", 9700m);
            var bidPrice = 10000;
            var bid = Bid.ByParticipant(participant).OfferedPriceForAsset(bidPrice, asset);
            Assert.IsNotNull(bid);
            Assert.IsTrue(bid.Price >= bidPrice);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void BidPriceMustNotBeLessThanAssetPrice()
        {
            var participant = new Participant("John Smith");
            var asset = new Asset("White House", 9700m);
            var bidPrice = asset.Price - 1;
            
            var bid = Bid.ByParticipant(participant).OfferedPriceForAsset(bidPrice, asset);
            Assert.Fail("Should throw exception on previous line");
        }
    }
}
