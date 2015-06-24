// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Person.cs" company="Uni-app">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ShowMeNow.API.Models.RelationModeles
{
    using System;

    public class Person
    {
        public Guid PersonId { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }
     
    }
}