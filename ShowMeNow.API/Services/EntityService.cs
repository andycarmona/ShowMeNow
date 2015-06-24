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

    using ShowMeNow.API.Models.Dto;
    using ShowMeNow.API.Models.RelationModeles;
    using ShowMeNow.API.Repositories;

    public class EntityService : IEntityService
    {
        private IPeopleNeo4JRepository _aPeopleRepository;

        private IPlacesNeo4JRepository _aPlaceRepository;

        public EntityService()
        {
            MapConfig();
        }

        public EntityService(IPeopleNeo4JRepository aPeopleRepository,IPlacesNeo4JRepository aPlaceRepository)
        {
            _aPeopleRepository = aPeopleRepository;
            _aPlaceRepository = aPlaceRepository;
            MapConfig();
        }

        public void MapConfig()
        {
            Mapper.CreateMap<List<Person>, List<PersonDto>>();
            Mapper.CreateMap<PersonDto, Person>();
            Mapper.CreateMap<PlaceDto, Place>();
        }

        public List<PersonDto> GetAllFriends(Guid personId)
        {
          
            List<Person> friends = _aPeopleRepository.GetAllFriends(personId);
            List<PersonDto> personFriends = Mapper.Map<List<PersonDto>>(friends);
            return personFriends;
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

        private void FindPerson(Person personToFind)
        {
            var foundPerson = this._aPeopleRepository.GetAPerson(personToFind.PersonId);
            if (foundPerson==null)
            {
                _aPeopleRepository.CreatePerson(personToFind);
            }
        }

        public void AddPlace(PlaceDto aPlace)
        {
            var entityPlace = Mapper.Map<Place>(aPlace);
            _aPlaceRepository.CreatePlace(entityPlace);
        }
    }
}