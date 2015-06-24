namespace ShowMeNow.API.Test.FakeDataModels
{
    using System;
    using System.Collections.Generic;

    using ShowMeNow.API.Models.Dto;
    using ShowMeNow.API.Models.RelationModeles;

    public class FakeDataHandler
    {
        public List<Place> GetPlace()
        {
            var listOfPlaces = new List<Place>()
                                           {
                                               new Place()
                                                   {
                                                       Address = "Chinese",
                                                       Coordinates = 2.3,
                                                       EMail = "rowe@se.se",
                                                       Type = Place.TypeOfPlace.Restaurant,
                                                       Name = "webo",
                                                       ParentName = string.Empty,
                                                       FeedbackId = 1,
                                                       PlaceId = Guid.NewGuid(),
                                                       Telephone = 22323456
                                                   },
                                               new Place()
                                                   {
                                                       Address = "Torget",
                                                       Coordinates = 2.3,
                                                       Type = Place.TypeOfPlace.Bar,
                                                       EMail = "we@se.se",
                                                       Name = "MyBar",
                                                       ParentName = string.Empty,
                                                       FeedbackId = 1,
                                                       PlaceId = Guid.NewGuid(),
                                                       Telephone = 2232378
                                                   },
                                                new Place()
                                                   {
                                                       Address = "Españolo",
                                                       Coordinates = 2.3,
                                                       Type = Place.TypeOfPlace.Restaurant,
                                                       EMail = "we@se.se",
                                                       Name = "MyBar",
                                                       ParentName = string.Empty,
                                                       FeedbackId = 1,
                                                       PlaceId = Guid.NewGuid(),
                                                       Telephone = 34522323
                                                   }
                                           };
            return listOfPlaces;
        }

        public Feedback GetFeedBack()
        {
            var aFeedback = new Feedback()
            {
                Comments = "loco",
                FeedbackId = 1,
                negativePunctuation = 3,
                positivePunctuation = 6
            };
            return aFeedback;
        }

        public Itinerary GetItinerary()
        {
            var aItinerary = new Itinerary() { ItineraryId = 2, Name = "loco viaje", Value = 25 };
            return aItinerary;
        }

        public List<Person> GetListOfPeople()
        {
           List<Person> listOfPeople;
            listOfPeople = new List<Person>();

            var person1 = new Person() { Age = 3, Email = "tes@se.se", Name = "Carlos", PersonId = Guid.NewGuid() };

            var person2 = new Person() { Age = 62, Email = "frt@se.se", Name = "Philip", PersonId = Guid.NewGuid() };

            var person3 = new Person() { Age = 52, Email = "frt@se.se", Name = "Malulo", PersonId = Guid.NewGuid() };

            listOfPeople.Add(person1);
            listOfPeople.Add(person2);
            listOfPeople.Add(person3);
            return listOfPeople;
        }


        public List<PersonDto> GetListOfDtoPeople()
        {
            List<PersonDto> listOfPeople;
            listOfPeople = new List<PersonDto>();

            var person1 = new PersonDto() { Age = 3, Email = "tes@se.se", Name = "Carlos", PersonId = Guid.NewGuid() };

            var person2 = new PersonDto() { Age = 62, Email = "frt@se.se", Name = "Philip", PersonId = Guid.NewGuid() };

            var person3 = new PersonDto() { Age = 52, Email = "frt@se.se", Name = "Malulo", PersonId = Guid.NewGuid() };

            listOfPeople.Add(person1);
            listOfPeople.Add(person2);
            listOfPeople.Add(person3);
            return listOfPeople;
        }

        public List<PlaceDto> GetDtoPlaces()
        {
            var listOfPlaces = new List<PlaceDto>()
                                           {
                                               new PlaceDto()
                                                   {
                                                       Address = "Chinese",
                                                       Coordinates = 2.3,
                                                       EMail = "rowe@se.se",
                                                       Type = Place.TypeOfPlace.Restaurant,
                                                       Name = "webo",
                                                       ParentName = string.Empty,
                                                       FeedbackId = 1,
                                                       PlaceId = 23,
                                                       Telephone = 22323456
                                                   },
                                               new PlaceDto()
                                                   {
                                                       Address = "Torget",
                                                       Coordinates = 2.3,
                                                       Type = Place.TypeOfPlace.Bar,
                                                       EMail = "we@se.se",
                                                       Name = "MyBar",
                                                       ParentName = string.Empty,
                                                       FeedbackId = 1,
                                                       PlaceId = 23,
                                                       Telephone = 2232378
                                                   },
                                                new PlaceDto()
                                                   {
                                                       Address = "Españolo",
                                                       Coordinates = 2.3,
                                                       Type = Place.TypeOfPlace.Restaurant,
                                                       EMail = "we@se.se",
                                                       Name = "MyBar",
                                                       ParentName = string.Empty,
                                                       FeedbackId = 1,
                                                       PlaceId = 23,
                                                       Telephone = 34522323
                                                   }
                                           };
            return listOfPlaces;
        }
    }
}
