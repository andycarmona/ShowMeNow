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
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using ShowMeNow.API.Models.RelationModeles;
    using ShowMeNow.API.Repositories;
    using ShowMeNow.API.Test.FakeDataModels;

    [TestClass]
    public class PlacesRepositoryTest
    {
        private IPlacesNeo4JRepository _placeRepository;
        private FakeDataHandler aFakeModel;

        public void InitializeDB()
        {
            this._placeRepository = new PlacesNeo4JRepository();
            this.aFakeModel = new FakeDataHandler();
            this._placeRepository.InitializeNeo4J();
        }

        [TestMethod]
        public void Test_Create_a_Place()
        {
            this.InitializeDB();
            var place1 = this.aFakeModel.GetPlace();
            this._placeRepository.CreatePlace(place1);
            var resultPlace = this._placeRepository.GetAPlace(place1.Name);
            Assert.AreEqual(place1.Name, resultPlace[0].Name);
            this._placeRepository.DeletePlace(place1.Name);
            resultPlace = this._placeRepository.GetAPlace(place1.Name);
            Assert.AreEqual(0 ,resultPlace.Count);
        }

        [TestMethod]
        public void Test_Create_LinkedList()
        {
            this.InitializeDB();
            var itinerary1 = this.aFakeModel.GetItinerary();
            var aItineray = this._placeRepository.CreateLinkedList(itinerary1);
        }

    }
}
