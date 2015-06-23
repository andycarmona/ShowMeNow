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
            
        }

        public EntityService(IPeopleNeo4JRepository aPeopleRepository,IPlacesNeo4JRepository aPlaceRepository)
        {
            _aPeopleRepository = aPeopleRepository;
            _aPlaceRepository = aPlaceRepository;
        }

        public List<PersonDto> GetAllFriends(string name)
        {
            Mapper.CreateMap<List<Person>, List<PersonDto>>();
            List<Person> friends = _aPeopleRepository.GetAllFriends(name);
            List<PersonDto> personFriends = Mapper.Map<List<PersonDto>>(friends);
            return personFriends;
        }
    }
}