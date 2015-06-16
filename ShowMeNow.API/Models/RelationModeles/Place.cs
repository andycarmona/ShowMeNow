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
    public class Place
    {
        public int PlaceId { get; set; }

        public float Coordinates { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public int Feedback { get; set; }

        public int Telephone { get; set; }

        public string EMail { get; set; }

    }
}