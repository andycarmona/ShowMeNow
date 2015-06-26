// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlacesService.cs" company="Uni-app">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ShowMeNow.API.Services
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;

    using ShowMeNow.API.Helpers;
    using ShowMeNow.API.Models.Dto;
    using ShowMeNow.API.Models.RelationModeles;
    using ShowMeNow.API.Repositories;

    public class EntityService : IEntityService
    {
        private IPeopleNeo4JRepository _aPeopleRepository;

        private IPlacesNeo4JRepository _aPlaceRepository;

        public EntityService()
        {
            _aPeopleRepository = new PeopleNeo4JRepository();
            _aPlaceRepository = new PlacesNeo4JRepository();
            MapConfig();
        }

        public EntityService(IPeopleNeo4JRepository aPeopleRepository,IPlacesNeo4JRepository aPlaceRepository)
        {
            _aPeopleRepository = aPeopleRepository;
            _aPlaceRepository = aPlaceRepository;
         
        }

        public void MapConfig()
        {
            Mapper.CreateMap<Person, PersonDto>();
            Mapper.CreateMap<PersonDto, Person>();
            Mapper.CreateMap<PlaceDto, Place>();
            Mapper.CreateMap<Place, PlaceDto>();
        }

        public List<PersonDto> GetAllFriends(string personId)
        {
            
            var friends = _aPeopleRepository.GetAllFriends(personId);
            var dtoPeopleFriendList = Mapper.Map<List<Person>, List<PersonDto>>(friends);

            return dtoPeopleFriendList;
        }

        public void AddPerson(PersonDto aPerson)
        {
            var entityPerson = Mapper.Map<Person>(aPerson);
            _aPeopleRepository.CreatePerson(entityPerson);

        }

        public void AddPersonRelation(PersonDto person1, PersonDto person2)
        {
            var entPerson1 = Mapper.Map<Person>(person1);
            var entPerson2 = Mapper.Map<Person>(person2);
            this.FindPerson(entPerson1);
            this.FindPerson(entPerson2);
           _aPeopleRepository.PersonKnowsPerson(entPerson1, entPerson2);
        }

        public void AddPlace(PlaceDto aPlace)
        {
            var entityPlace = Mapper.Map<Place>(aPlace);
            _aPlaceRepository.CreatePlace(entityPlace);
        }

        public List<PlaceDto> GetAllPlaces()
        {
            MapConfig();
            var allPlaces = _aPlaceRepository.GetAllPlaces();
         
            List<PlaceDto> dtoPlaceList = Mapper.Map<List<Place>, List<PlaceDto>>(allPlaces);
             

            return dtoPlaceList;
        }

        public List<PersonDto> GetAllPeople()
        {
            var allPeople = _aPeopleRepository.GetAllPeople();
            var dtoPeopleList = Mapper.Map<List<Person>, List<PersonDto>>(allPeople);
            
            return dtoPeopleList;
        }

        private void FindPerson(Person personToFind)
        {
            var foundPerson = this._aPeopleRepository.GetAPerson(personToFind.PersonId);
            if (foundPerson == null)
            {
                _aPeopleRepository.CreatePerson(personToFind);
            }
        }
    }
}