namespace Implementation.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Auction : ModelWithIdentity
    {
        public Auction(string name)
        {
            AvailableAssets = new List<Asset>();
            Package = new Package(name);
            Participants = new List<Participant>();
            Rounds = new List<Round>();
        }

        public string Name
        {
            get { return Package.Name; }
        }

        public void AddAsset(Asset asset)
        {
            VerifyThatAuctionHasNotStarted();
            AvailableAssets.Add(asset);
        }

        public void AddAssetToPackage(Asset asset, Package package)
        {
            VerifyThatAuctionHasNotStarted();
            VerifyThatPackageIsPartOfAuction(package);
            package.AddAsset(package);
        }

        public void AddPackage(Package package)
        {
            VerifyThatAuctionHasNotStarted();
            Package.Contents.Add(package);
        }

        public void AddParticipant(Participant participant)
        {
            VerifyThatAuctionHasNotStarted();
            Participants.Add(participant);
        }

        public void Bid(Asset asset, Participant participant, decimal price)
        {
            var bid = Models.Bid.ByParticipant(participant).OfferedPriceForAsset(price, asset);
            CurrentRound[participant].BidForAsset(bid, asset);
        }

        public IEnumerable<Asset> GetAllAssets()
        {
            return Package.GetAllAssets();
        }

        #region data

        public List<Asset> AvailableAssets { get; private set; }

        public Round CurrentRound { get; private set; }

        public Package Package { get; private set; }

        public List<Participant> Participants { get; private set; }

        public List<Round> Rounds { get; private set; }

        #endregion data
        public bool HasStarted()
        {
            return Rounds.Any();
        }

        public void StartNextRound()
        {
            VerifyThatAllUsersHaveSubmittedBids();
            CalculatePrices();
            CurrentRound = new Round(Participants);
            Rounds.Add(CurrentRound);
        }

        private void CalculatePrices()
        {
            Package.CalculateTotalPrice();
        }

        #region validation

        private void VerifyThatAllUsersHaveSubmittedBids()
        {
            if (CurrentRound != null && CurrentRound.GetParticipantsWhichMustSubmitBid().Any())
            {
                throw new InvalidOperationException("Cannot start next round untill all participants have submitted bids for current round.");
            }
        }

        private void VerifyThatAuctionHasNotStarted()
        {
            if (HasStarted())
            {
                throw new InvalidOperationException("Participants cannot be added to auction once it has started");
            }
        }

        private void VerifyThatPackageIsPartOfAuction(Package package)
        {
            if (!GetAllAssets().Any(item => item.Equals(package)))
            {
                throw new ArgumentOutOfRangeException("package", "Cannot add asset to package which is not part of auction!");
            }
        }

        #endregion validation

        public class Round : Dictionary<Participant, ParticipantBids>
        {
            internal Round(List<Participant> participants)
            {
                participants.ForEach(participant => Add(participant, new ParticipantBids(new List<Asset>())));
            }

            public IEnumerable<Participant> GetParticipantsWhichMustSubmitBid()
            {
                return GetParticipantsWithoutAnyBids()
                    .Union(GetParticipantsWithMissingBidsForAssets())
                    .Distinct();
            }

            private IEnumerable<Participant> GetParticipantsWithMissingBidsForAssets()
            {
                return this.Where(item => item.Value.Any(assetBid => assetBid.Value == null)).Select(item => item.Key);
            }

            private IEnumerable<Participant> GetParticipantsWithoutAnyBids()
            {
                return this.Where(item => !item.Value.Any())
                    .Select(item => item.Key);
            }
        }
    }
}