namespace Implementation.Models
{
    using System;

    public class Bid
    {
        protected Bid(Participant participant, Asset asset, decimal bidPrice)
        {
            VerifyThatBidIsValid(asset, bidPrice);
            Asset = asset;
            Participant = participant;
            Price = bidPrice;
            SubmitteDateTime = DateTime.Now;
        }

        public Asset Asset { get; private set; }

        public Participant Participant { get; private set; }

        public decimal Price { get; private set; }

        public DateTime SubmitteDateTime { get; private set; }

        public static ParticipantBidBuilder ByParticipant(Participant participant)
        {
            return new ParticipantBidBuilder(participant);
        }

        private static void VerifyThatBidIsValid(Asset asset, decimal bidPrice)
        {
            if (bidPrice < asset.Price)
            {
                throw new ArgumentOutOfRangeException("bidPrice", bidPrice,
                    string.Format("Bid price must be greater than or equal to predefined asset price ({0})", asset.Price));
            }
        }

        public class ParticipantBidBuilder
        {
            private readonly Participant _participant;

            public ParticipantBidBuilder(Participant participant)
            {
                _participant = participant;
            }

            public Bid OfferedPriceForAsset(decimal bidPrice, Asset asset)
            {
                return new Bid(_participant, asset, bidPrice);
            }
        }
    }
}