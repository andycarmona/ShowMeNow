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
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using ShowMeNow.API.Models.RelationModeles;
    using ShowMeNow.API.Repositories;
    using ShowMeNow.API.Test.FakeDataModels;

    [TestClass]
    public class PeopleRepositoryTest
    {
        private IPeopleNeo4JRepository _peopleRepository;
        private FakeDataHandler aFakeModel;

        private List<Person> listPeople;

        [TestInitialize]
        public void TestInitialize()
        {
            this._peopleRepository = new PeopleNeo4JRepository();
            this.aFakeModel = new FakeDataHandler();
            this._peopleRepository.InitializeNeo4J();
            listPeople = this.aFakeModel.GetListOfPeople();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            listPeople = null;
            //this.DeleteAllPeople();
        }


        public void DeleteAllPeople()
        {
            var allPeople = _peopleRepository.GetAllPeople();
            foreach (var aPerson in allPeople)
            {
                _peopleRepository.DeletePerson(aPerson.PersonId);
            }
        }

        [TestMethod]
        public void Test_Create_Person_and_Get_Information()
        {
            this._peopleRepository.CreatePerson(listPeople[0]);

            var listOfPeople = this._peopleRepository.GetAPerson(this.listPeople[0].PersonId);

            Assert.AreEqual(listPeople[0].Email, listOfPeople[0].Email);
        }

        [TestMethod]
        public void Test_Creating_and_Deleting_People_with_Relations()
        {
            foreach (var person in listPeople)
            {
                this._peopleRepository.CreatePerson(person);
            }

            this._peopleRepository.PersonKnowsPerson(listPeople[0], listPeople[1]);
            this._peopleRepository.PersonKnowsPerson(listPeople[1], listPeople[2]);
            var listOfFriends = this._peopleRepository.GetAllFriends(this.listPeople[0].PersonId);
            if (listOfFriends.Count > 0)
            {
                Assert.AreEqual(listOfFriends[0].Email, listPeople[0].Email);
            }
        }

        [TestMethod]
        public void Test_Creating_And_Deleting_one_person()
        {
            this._peopleRepository.CreatePerson(listPeople[0]);

            var qtyBeforeDel = this._peopleRepository.GetAllPeople().Count;
            this._peopleRepository.DeletePerson(this.listPeople[0].PersonId);
            var qtyAfterDel = this._peopleRepository.GetAllPeople().Count;
            Assert.IsNotNull(qtyAfterDel);
            Assert.IsInstanceOfType(qtyAfterDel, typeof(int));
            Assert.AreEqual(qtyBeforeDel, qtyAfterDel + 1);
        }

        [TestMethod]
        public void Test_Update_Person_Node()
        {
                foreach (var person in listPeople)
            {
                this._peopleRepository.CreatePerson(person);
            }

            this._peopleRepository.PersonKnowsPerson(listPeople[0], listPeople[1]);
            this._peopleRepository.PersonKnowsPerson(listPeople[1], listPeople[2]);
      
            var aPerson = _peopleRepository.GetAPerson(listPeople[0].PersonId);
            Assert.AreEqual(listPeople[0].Name, aPerson[0].Name);
            _peopleRepository.UpdatePersonName(aPerson[0].PersonId, "Pepe");
            aPerson = _peopleRepository.GetAPerson(listPeople[0].PersonId);
            Assert.AreEqual(aPerson[0].Name, "Pepe");
        }

        [TestMethod]
        public void Test_Update_All_Properties_Person_Node()
        {
            foreach (var person in listPeople)
            {
                this._peopleRepository.CreatePerson(person);
            }
            this._peopleRepository.PersonKnowsPerson(listPeople[0], listPeople[1]);
            this._peopleRepository.PersonKnowsPerson(listPeople[1], listPeople[2]);
            var newPerson = new Person() { Age = 10, Email = "fiolo@te.se", Name = "Testeo" };
            var aPerson = _peopleRepository.GetAPerson(listPeople[0].PersonId);
            Assert.IsNotNull(aPerson);
            Assert.IsInstanceOfType(aPerson, typeof(List<Person>));
            Assert.AreEqual(listPeople[0].Name, aPerson[0].Name);
            Assert.AreEqual(listPeople[0].Age, aPerson[0].Age);
            Assert.AreEqual(listPeople[0].Email, aPerson[0].Email);
            _peopleRepository.UpdatePersonProperties(aPerson[0].PersonId, newPerson);
            aPerson = _peopleRepository.GetAPerson(listPeople[0].PersonId);
            Assert.IsNotNull(aPerson);
            Assert.AreEqual(aPerson[0].Name, newPerson.Name);
            Assert.AreEqual(aPerson[0].Age, newPerson.Age);
            Assert.AreEqual(aPerson[0].Email, newPerson.Email);
        }
    }
}
