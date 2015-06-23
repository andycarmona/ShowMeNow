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

    public interface IEntityService
    {
    List<PersonDto> GetAllFriends(string name);
    }
}
