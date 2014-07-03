namespace WebEndpoint.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Implementation.Models;

    public class AuctionRepository
    {
        private static List<Auction> Auctions { get; set; }

        static AuctionRepository()
        {
            Auctions = new List<Auction>();
        }

        public static void Add(Auction auction)
        {
            lock (Auctions)
            {
                if (!Auctions.Contains(auction))
                    Auctions.Add(auction); 
            }
        }

        public static List<Auction> GetAll()
        {
            return Auctions;
        }

        public static Auction Get(Guid id)
        {
            return Auctions.Single(auction => auction.Id == id);
        }
    }
}