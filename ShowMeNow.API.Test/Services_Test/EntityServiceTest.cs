using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ShowMeNow.API.Test.Services_Test
{
    using System.Collections.Generic;

    using Rhino.Mocks;
    using Rhino.Mocks.Constraints;

    using ShowMeNow.API.Models.Dto;
    using ShowMeNow.API.Models.RelationModeles;
    using ShowMeNow.API.Repositories;
    using ShowMeNow.API.Services;
    using ShowMeNow.API.Test.FakeDataModels;

    [TestClass]
    public class EntityServiceTest
    {
        private IPeopleNeo4JRepository _mockPeopleRepository;
        private IPlacesNeo4JRepository _mockPlaceRepository;

        private IEntityService aEntityService;
   

        [TestInitialize]
        public void TestInitialize()
        {
            _mockPeopleRepository = MockRepository.GenerateMock<IPeopleNeo4JRepository>();
            _mockPlaceRepository = MockRepository.GenerateMock<IPlacesNeo4JRepository>();
            aEntityService = new EntityService(_mockPeopleRepository, _mockPlaceRepository);
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            _mockPeopleRepository = null;
            _mockPlaceRepository = null;
        }

        [TestMethod]
        public void EntityService_calls_Repository_methods()
        {
            _mockPeopleRepository.Stub(x => x.GetAllFriends("Philip")).Return(null);
            aEntityService.GetAllFriends("Philip");
            _mockPeopleRepository.AssertWasCalled(x => x.GetAllFriends("Philip"));
        }

        [TestMethod]
        public void EntiyService_Returns_List_With_Valid_Values()
        {
            var aFakeDataHandler = new FakeDataHandler();
            var listOfPeople = aFakeDataHandler.GetListOfPeople();

            _mockPeopleRepository.Stub(x => x.GetAllFriends(Arg<string>.Is.Anything)).Return(listOfPeople);
            var listOfFriends = aEntityService.GetAllFriends("Philip");
            Assert.IsInstanceOfType(listOfFriends, typeof(List<PersonDto>));
            Assert.AreEqual("Carlos", listOfFriends[0].Name);
        }
    }
}
