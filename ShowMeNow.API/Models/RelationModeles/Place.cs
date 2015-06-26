// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Place.cs" company="Uni-app">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ShowMeNow.API.Models.RelationModeles
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Place
    {
        public enum TypeOfPlace
        {
            Restaurant,
            Museum,
            Disco,
            Bar,
            Cinema
        };

        [Key]
        public string PlaceId { get; set; }

        public TypeOfPlace Type { get; set; }

        public double Coordinates { get; set; }

          [MaxLength(40)]
        public string Name { get; set; }

          [MaxLength(40)]
        public string Address { get; set; }

        public int FeedbackId { get; set; }

        public int Telephone { get; set; }

        public string EMail { get; set; }

        public string ParentName { get; set; }

    
    }
}