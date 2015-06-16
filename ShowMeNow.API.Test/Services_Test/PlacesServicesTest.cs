namespace ShowMeNow.API.Test.Services_Test
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Neo4jClient;

    using ShowMeNow.API.Models.RelationModeles;
    using ShowMeNow.API.Services;

    [TestClass]
    public class PlacesServicesTest
    {
        private IPlacesService _placeService;

        public void InitializeDB()
        {
            _placeService = new PlacesServices();

            this._placeService.InitializeNeo4J();
        }

        /*
    * Creates initial graph in DB
    * */
        public void CreateInitialData()
        {
            InitializeDB();

            // Create entities
            var refA =
                _placeService.CreatePerson(
                    new Person() { Age = 12, Email = "we@se.se", Name = "Antonio", PersonId = 1 });

            var refB =
                _placeService.CreatePerson(
                    new Person() { Age = 42, Email = "wewq@se.se", Name = "Carlos", PersonId = 2 });

            var refC =
               _placeService.CreatePerson(
                new Person() { Age = 32, Email = "dfrer@se.se", Name = "Luis", PersonId = 3 });

            var refD = _placeService.CreatePerson(new Person() { Age = 42, Email = "lovo@se.se", Name = "Sara", PersonId = 5 });

            // Create relationships
            //_placeService.PeoplesHatesRelationShip(refB, refD, "crazy guy");
            //_placeService.PeoplesHatesRelationShip(refC, refD, "Don't know why");
        
        }

        [TestMethod]
        public void Test_Create_Person_and_Get_Information()
        {

            InitializeDB();
            _placeService.CreatePerson(
                new Person() { Age = 12, Email = "we@se.se", Name = "Antonio", PersonId = 4 });

            var listOfPeople = _placeService.GetAPerson("Antonio");

            Assert.AreEqual("we@se.se", listOfPeople[0].Email);

            this.DeletePerson(listOfPeople[0].Name);


        }

        [TestMethod]
        public void Test_Creating_Relations_between_People()
        {
            InitializeDB();

            var person1 = new Person() { Age = 3, Email = "tes@se.se", Name = "Carlos", PersonId = 3 };
            _placeService.CreatePerson(person1);
            var person2 = new Person() { Age = 62, Email = "frt@se.se", Name = "Philip", PersonId = 4 };
            _placeService.CreatePerson(person2);

            _placeService.PersonKnowsPerson(person1, person2);


            DeletePerson(person1.Name);
         DeletePerson(person2.Name);
        }



        public void DeletePerson(string name)
        {
            var result = _placeService.GetAllFriends(name);

            if (result.Count <1)
            {
                _placeService.DeletePerson(name);
               
            }
            else
            {
                _placeService.DeletePersonAndRelations(name);
            }
        }


    }
}
