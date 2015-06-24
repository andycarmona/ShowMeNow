// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPlacesService.cs" company="Uni-app">
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

    using ShowMeNow.API.Models.Dto;

    public interface IEntityService
    {
    List<PersonDto> GetAllFriends(Guid personId);

        void AddPerson(PersonDto aPerson);

        void AddPersonRelation(PersonDto person1, PersonDto person2);

        void AddPlace(PlaceDto aPlace);
    }
}
