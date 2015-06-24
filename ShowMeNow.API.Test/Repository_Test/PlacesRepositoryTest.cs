// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlacesRepositoryTest.cs" company="Uniapp">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ShowMeNow.API.Test.Repository_Test
{
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using ShowMeNow.API.Models.RelationModeles;
    using ShowMeNow.API.Repositories;
    using ShowMeNow.API.Test.FakeDataModels;

    [TestClass]
    public class PlacesRepositoryTest
    {
        private IPlacesNeo4JRepository _placeRepository;
        private FakeDataHandler aFakeModel;

        private List<Place> listOfPlaces;

        [TestInitialize]
        public void InitializeDB()
        {
            this._placeRepository = new PlacesNeo4JRepository();
            this.aFakeModel = new FakeDataHandler();
            this._placeRepository.InitializeNeo4J();

             listOfPlaces = this.aFakeModel.GetPlace();
            foreach (var aPlace in listOfPlaces)
            {
                this._placeRepository.CreatePlace(aPlace);
            }
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            var allPlaces = this._placeRepository.GetAllPlaces();
            foreach (var aPlace in allPlaces)
            {
                this._placeRepository.DeletePlace(aPlace.PlaceId);
            }
           
            var resultPlaces = this._placeRepository.GetAllPlaces();
            Assert.AreEqual(0, resultPlaces.Count);
        }

        [TestMethod]
        public void Test_Create_a_Place()
        {
            var resultPlace = this._placeRepository.GetAPlace(this.listOfPlaces[0].PlaceId);
            Assert.AreEqual(listOfPlaces[0].Name, resultPlace[0].Name);
        
        }
        [TestMethod]
        public void Test_Update_Person_Node()
        {
            foreach (var aPlace in listOfPlaces)
            {
                this._placeRepository.CreatePlace(aPlace);
            }


            var onePlace = _placeRepository.GetAPlace(this.listOfPlaces[0].PlaceId);
            Assert.AreEqual(listOfPlaces[0].Name, onePlace[0].Name);
            _placeRepository.UpdatePlaceName(onePlace[0].PlaceId, "Loco mia");
            onePlace = _placeRepository.GetAPlace(this.listOfPlaces[0].PlaceId);
            Assert.AreEqual(onePlace[0].Name, "Loco mia");
        }

        [TestMethod]
        public void Test_Update_All_Properties_Person_Node()
        {
            foreach (var aPlace in listOfPlaces)
            {
                this._placeRepository.CreatePlace(aPlace);
            }
            
            var onePlace = _placeRepository.GetAPlace(listOfPlaces[0].PlaceId);
            Assert.IsNotNull(onePlace);
            Assert.IsInstanceOfType(onePlace, typeof(List<Place>));
            Assert.AreEqual(listOfPlaces[0].Name, onePlace[0].Name);
            Assert.AreEqual(listOfPlaces[0].EMail, onePlace[0].EMail);
            Assert.AreEqual(listOfPlaces[0].Address, onePlace[0].Address);
            _placeRepository.UpdatePlaceProperties(listOfPlaces[0].PlaceId, onePlace[0]);
            onePlace = _placeRepository.GetAPlace(listOfPlaces[0].PlaceId);
            Assert.IsNotNull(onePlace);
            Assert.AreEqual(listOfPlaces[0].Name, onePlace[0].Name);
            Assert.AreEqual(listOfPlaces[0].EMail, onePlace[0].EMail);
            Assert.AreEqual(listOfPlaces[0].Address, onePlace[0].Address);
        }
    }
}
