namespace WebEndpoint.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using Implementation.Models;
    using Storage;

    public class AuctionController : ApiController
    {
        public Auction Get(Guid id)
        {
            return AuctionRepository.Get(id);
        }

        public Auction Create(string name)
        {
            var auction = new Auction(name);
            AuctionRepository.Add(auction);
            return auction;
        }

        public Participant AddParticipant(Guid toAuctionId, string name)
        {
            var participant = new Participant(name);
            var auction = Get(toAuctionId);
            auction.AddParticipant(participant);
            return participant;
        }

        public Asset AddAsset(Guid toAuctionId, string name, decimal price)
        {
            var asset = new Asset(name, price);
            var auction = Get(toAuctionId);
            auction.AddAsset(asset);
            return asset;
        }

        public Package AddPackage(Guid toAuctionId, string packageName, List<Guid> assetIdsToAdd)
        {
            var auction = Get(toAuctionId);
            var assets = auction.AvailableAssets.Where(asset => assetIdsToAdd.Contains(asset.Id)).ToList();
            var package = new Package(packageName, assets);
            auction.AddPackage(package);
            return package;
        }
    }
}
