using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Integration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Implementation.Models;
    using WebEndpoint.Controllers;

    [TestClass]
    public class AuctionControllerTest
    {
        private AuctionController _auctionController;
        private string _testAuctionName = "test auction";

        [TestInitialize]
        public void SetUp()
        {
            _auctionController = new AuctionController();
        }

        [TestMethod]
        public void AuctionCreate()
        {
            var auction = _auctionController.Create(_testAuctionName);
            Assert.IsNotNull(auction);
            Assert.AreEqual(_testAuctionName, auction.Name);
        }

        [TestMethod]
        public void GetAuction()
        {
            var expectedAuction = _auctionController.Create(_testAuctionName);

            var actualAuction = _auctionController.Get(expectedAuction.Id);

            Assert.AreEqual(expectedAuction.Id, actualAuction.Id); //could compare references too as they are persisted in memory
        }

        [TestMethod]
        public void AddParticipant()
        {
            var auction = _auctionController.Create(_testAuctionName);
            const string participantName = "Participant 1";
            _auctionController.AddParticipant(auction.Id, participantName);

            var addedParticipant = _auctionController.Get(auction.Id).Participants.Find(item => item.Name == participantName);

            Assert.AreEqual(participantName, addedParticipant.Name);
        }



        [TestMethod]
        public void AddPackage()
        {
            var auction = _auctionController.Create(_testAuctionName);
            var assetIds = new List<Guid>
            {
                _auctionController.AddAsset(auction.Id, "asset 1", 100).Id,
                _auctionController.AddAsset(auction.Id, "asset 2", 200).Id,
                _auctionController.AddAsset(auction.Id, "asset 3", 300).Id,
            };
            var independentAsset1 = _auctionController.AddAsset(auction.Id, "asset 4", 300);
            var independentAsset2 = _auctionController.AddAsset(auction.Id, "asset 5", 300);
            var package = _auctionController.AddPackage(auction.Id, "test package 1", assetIds);

            var updatedAuction = _auctionController.Get(auction.Id);
            Assert.IsTrue(auction.Package.Contents.Any(item=>item.Id == package.Id));
        }
    }
}
