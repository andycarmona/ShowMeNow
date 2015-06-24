// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Itinerary.cs" company="Uni-app">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ShowMeNow.API.Models.RelationModeles
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Itinerary
    {
         [Key] 
        public int ItineraryId { get; set; }

        [MaxLength(40)]
        public string Name { get; set; }

        public int Value { get; set; }
    }
}