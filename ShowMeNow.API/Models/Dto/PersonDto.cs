// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Person.cs" company="Uni-app">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ShowMeNow.API.Models.Dto
{
    using System;

    public class PersonDto
    {
        public Guid PersonId { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }
     
    }
}