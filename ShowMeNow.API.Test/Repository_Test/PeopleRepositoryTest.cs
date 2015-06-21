// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PeopleRepositoryTest.cs" company="Uniapp">
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
    public class PeopleRepositoryTest
    {
        private IPeopleNeo4JRepository _peopleRepository;
        private FakeDataHandler aFakeModel;

        public void InitializeDB()
        {
            this._peopleRepository = new PeopleNeo4JRepository();
            this.aFakeModel = new FakeDataHandler();
            this._peopleRepository.InitializeNeo4J();
        }

        /*
    * Creates initial graph in DB
    * */
        public void CreateInitialData()
        {
            this.InitializeDB();

            // Create entities
            var refA =
                this._peopleRepository.CreatePerson(
                    new Person() { Age = 12, Email = "we@se.se", Name = "Antonio", PersonId = 1 });

            var refB =
                this._peopleRepository.CreatePerson(
                    new Person() { Age = 42, Email = "wewq@se.se", Name = "Carlos", PersonId = 2 });

            var refC =
               this._peopleRepository.CreatePerson(
                new Person() { Age = 32, Email = "dfrer@se.se", Name = "Luis", PersonId = 3 });

            var refD = this._peopleRepository.CreatePerson(new Person() { Age = 42, Email = "lovo@se.se", Name = "Sara", PersonId = 5 });

            // Create relationships
            //_peopleRepository.PeoplesHatesRelationShip(refB, refD, "crazy guy");
            //_peopleRepository.PeoplesHatesRelationShip(refC, refD, "Don't know why");

        }

        [TestMethod]
        public void Test_Create_Person_and_Get_Information()
        {

            this.InitializeDB();
            var listPeople = this.aFakeModel.GetListOfPeople();
            this._peopleRepository.CreatePerson(listPeople[0]);

            var listOfPeople = this._peopleRepository.GetAPerson(listPeople[0].Name);

            Assert.AreEqual(listPeople[0].Email, listOfPeople[0].Email);

            this._peopleRepository.DeletePerson(listOfPeople[0].Name);


        }

        [TestMethod]
        public void Test_Creating_And_Deleting_People_with_Relations()
        {
            this.InitializeDB();
            var listPeople = this.aFakeModel.GetListOfPeople();
            foreach (var person in listPeople)
            {
                this._peopleRepository.CreatePerson(person);
            }

            this._peopleRepository.PersonKnowsPerson(listPeople[0], listPeople[1]);
            this._peopleRepository.PersonKnowsPerson(listPeople[1], listPeople[2]);
            var listOfFriends = this._peopleRepository.GetAllFriends(listPeople[0].Name);
            if (listOfFriends.Count > 0)
            {
                Assert.AreEqual(listOfFriends[0].Email, listPeople[1].Email);
            }

            var qtyBeforeDel = this._peopleRepository.GetAllPeople().Count;
            this._peopleRepository.DeletePerson(listPeople[0].Name);
            var qtyAfterDel = this._peopleRepository.GetAllPeople().Count;
            Assert.AreEqual(qtyBeforeDel, qtyAfterDel + 1);

            qtyBeforeDel = this._peopleRepository.GetAllPeople().Count;
            this._peopleRepository.DeletePerson(listPeople[1].Name);
            qtyAfterDel = this._peopleRepository.GetAllPeople().Count;
            Assert.AreEqual(qtyBeforeDel, qtyAfterDel + 1);

            qtyBeforeDel = this._peopleRepository.GetAllPeople().Count;
            this._peopleRepository.DeletePerson(listPeople[2].Name);
            qtyAfterDel = this._peopleRepository.GetAllPeople().Count;
            Assert.AreEqual(qtyBeforeDel, qtyAfterDel + 1);
        }

        [TestMethod]
        public void Test_Creating_And_Deleting_one_person()
        {
            this.InitializeDB();
            var listPeople = this.aFakeModel.GetListOfPeople();
        
                this._peopleRepository.CreatePerson(listPeople[0]);

            var qtyBeforeDel = this._peopleRepository.GetAllPeople().Count;
            this._peopleRepository.DeletePerson(listPeople[0].Name);
            var qtyAfterDel = this._peopleRepository.GetAllPeople().Count;
            Assert.AreEqual(qtyBeforeDel, qtyAfterDel + 1);
        }

        [TestMethod]
        public void Test_Deleting_Nodes_WithRelationShips()
        {
            this.InitializeDB();

            var listOfPeople = this._peopleRepository.GetAllPeople();

            foreach (var person in listOfPeople)
            {
                Assert.IsNotNull(person);
                Assert.IsInstanceOfType(person, typeof(Person));
                //  DeletePerson(person.Name);
            }
        }
    }
}
