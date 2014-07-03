namespace Tests.Unit
{
    using System;
    using Implementation.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AuctionTests
    {
        [TestMethod]
        public void AddParticipant()
        {
            var auction = new Auction("test auction");
            auction.AddParticipant(new Participant("John Smith"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShouldNotBePossibleToAddParticipantAfterAuctionStart()
        {
            var auction = new Auction("test auction");
            auction.StartNextRound();
            auction.AddParticipant(new Participant("John Smith"));
        }

    }
}