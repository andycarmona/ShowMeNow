﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Place.cs" company="Uni-app">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ShowMeNow.API.Models.Dto
{
    public class PlaceDto
    {
        public int PlaceId { get; set; }

        public double Coordinates { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public int FeedbackId { get; set; }

        public int Telephone { get; set; }

        public string EMail { get; set; }

           public string ParentName { get; set; }

    }
}