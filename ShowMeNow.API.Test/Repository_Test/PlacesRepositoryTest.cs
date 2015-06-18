namespace ShowMeNow.API.Test.Services_Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Neo4jClient;

    using ShowMeNow.API.Models;
    using ShowMeNow.API.Models.RelationModeles;
    using ShowMeNow.API.Repositories;
    using ShowMeNow.API.Test.FakeDataModels;

    [TestClass]
    public class PlacesRepositoryTest
    {
        private IPlacesRepository _placeRepository;
        private FakeDataHandler aFakeModel;

        public void InitializeDB()
        {
            this._placeRepository = new PlacesRepository();
            aFakeModel = new FakeDataHandler();
            this._placeRepository.InitializeNeo4J();
        }

        /*
    * Creates initial graph in DB
    * */
        public void CreateInitialData()
        {
            InitializeDB();

            // Create entities
            var refA =
                this._placeRepository.CreatePerson(
                    new Person() { Age = 12, Email = "we@se.se", Name = "Antonio", PersonId = 1 });

            var refB =
                this._placeRepository.CreatePerson(
                    new Person() { Age = 42, Email = "wewq@se.se", Name = "Carlos", PersonId = 2 });

            var refC =
               this._placeRepository.CreatePerson(
                new Person() { Age = 32, Email = "dfrer@se.se", Name = "Luis", PersonId = 3 });

            var refD = this._placeRepository.CreatePerson(new Person() { Age = 42, Email = "lovo@se.se", Name = "Sara", PersonId = 5 });

            // Create relationships
            //_placeRepository.PeoplesHatesRelationShip(refB, refD, "crazy guy");
            //_placeRepository.PeoplesHatesRelationShip(refC, refD, "Don't know why");

        }

        [TestMethod]
        public void Test_Create_Person_and_Get_Information()
        {

            InitializeDB();
            var listPeople = aFakeModel.GetListOfPeople();
            this._placeRepository.CreatePerson(listPeople[0]);

            var listOfPeople = this._placeRepository.GetAPerson(listPeople[0].Name);

            Assert.AreEqual(listPeople[0].Email, listOfPeople[0].Email);

            _placeRepository.DeletePerson(listOfPeople[0].Name);


        }

        [TestMethod]
        public void Test_Creating_Relations_between_People()
        {
            InitializeDB();
            var listPeople = aFakeModel.GetListOfPeople();
            foreach (var person in listPeople)
            {
                this._placeRepository.CreatePerson(person);
            }

            this._placeRepository.PersonKnowsPerson(listPeople[0], listPeople[1]);
            this._placeRepository.PersonKnowsPerson(listPeople[1], listPeople[2]);
            var listOfFriends = this._placeRepository.GetAllFriends(listPeople[0].Name);
            if (listOfFriends.Count > 0)
            {
                Assert.AreEqual(listOfFriends[0].Email, listPeople[1].Email);
            }

            var qtyBeforeDel = _placeRepository.GetAllPeople().Count;
            _placeRepository.DeletePerson(listPeople[0].Name);
            var qtyAfterDel = _placeRepository.GetAllPeople().Count;
            Assert.AreEqual(qtyBeforeDel, qtyAfterDel + 1);

            qtyBeforeDel = _placeRepository.GetAllPeople().Count;
            _placeRepository.DeletePerson(listPeople[1].Name);
            qtyAfterDel = _placeRepository.GetAllPeople().Count;
            Assert.AreEqual(qtyBeforeDel, qtyAfterDel + 1);

            qtyBeforeDel = _placeRepository.GetAllPeople().Count;
            _placeRepository.DeletePerson(listPeople[2].Name);
            qtyAfterDel = _placeRepository.GetAllPeople().Count;
            Assert.AreEqual(qtyBeforeDel, qtyAfterDel + 1);
        }



        [TestMethod]
        public void Test_Create_Places_And_Relations()
        {
            this.InitializeDB();
            var place1 = aFakeModel.GetPlace();
            this._placeRepository.CreatePlace(place1);
            var resultPlace = this._placeRepository.GetAPlace(place1.Name);
            Assert.AreEqual(place1.Name, resultPlace[0].Name);
            this._placeRepository.DeletePlace(place1.Name);
        }

        [TestMethod]
        public void Test_Deleting_Nodes_WithRelationShips()
        {
            this.InitializeDB();

            var listOfPeople = this._placeRepository.GetAllPeople();

            foreach (var person in listOfPeople)
            {
                Assert.IsNotNull(person);
                Assert.IsInstanceOfType(person, typeof(Person));
                //  DeletePerson(person.Name);
            }
        }
    }
}
