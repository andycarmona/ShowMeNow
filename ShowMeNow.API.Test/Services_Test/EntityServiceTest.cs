namespace ShowMeNow.API.Test.Services_Test
{
    using System;
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            var personId = Guid.NewGuid().ToString();
            _mockPeopleRepository.Stub(x => x.GetAllFriends(personId)).Return(null);
            aEntityService.GetAllFriends(personId);
            _mockPeopleRepository.AssertWasCalled(x => x.GetAllFriends(personId));
        }

        [TestMethod]
        public void Test_EntityService_Returns_List_With_Valid_Values()
        {
            var personId = Guid.NewGuid().ToString();
            var aFakeDataHandler = new FakeDataHandler();
            var listOfPeople = aFakeDataHandler.GetListOfPeople();

            _mockPeopleRepository.Stub(x => x.GetAllFriends(Arg<string>.Is.Anything)).Return(listOfPeople);
            var listOfFriends = aEntityService.GetAllFriends(personId);
            Assert.IsInstanceOfType(listOfFriends, typeof(List<PersonDto>));
          
        }

        [TestMethod]
        public void Test_to_Get_All_Places()
        {
            var aService = new EntityService();
            var listofPlaces = aService.GetAllPlaces();
            Assert.IsNotNull(listofPlaces);
        }
        
    }
}
