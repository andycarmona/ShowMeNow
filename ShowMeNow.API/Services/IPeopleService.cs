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
    using System.Collections.Generic;

    using ShowMeNow.API.Models.Dto;

    public interface IPeopleService
    {
    List<PersonPlaceDto> GetAllFriends(string name);
    }
}
