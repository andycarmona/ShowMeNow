namespace ShowMeNow.API.Test.FakeDataModels
{
    using System.Collections.Generic;

    using ShowMeNow.API.Models.RelationModeles;

    class FakeDataHandler
    {
        public Place GetPlace()
        {
       
            var place1 = new Place()
            {
                Address = "torget",
                Coordinates = 2.3,
                EMail = "we@se.se",
                Name = "webo",
                ParentName = string.Empty,
                FeedbackId = 1,
                PlaceId = 23,
                Telephone = 22323
            };
            return place1;
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
            var aItinerary = new Itinerary() { ItineraryId = 2, Name = "loco viaje" };
            return aItinerary;
        }

        public List<Person> GetListOfPeople()
        {
           List<Person> listOfPeople;
            listOfPeople = new List<Person>();

            var person1 = new Person() { Age = 3, Email = "tes@se.se", Name = "Carlos", PersonId = 3 };
          
            var person2 = new Person() { Age = 62, Email = "frt@se.se", Name = "Philip", PersonId = 4 };
           
            var person3 = new Person() { Age = 52, Email = "frt@se.se", Name = "Malulo", PersonId = 6 };

            listOfPeople.Add(person1);
            listOfPeople.Add(person2);
            listOfPeople.Add(person3);
            return listOfPeople;
        } 
    }
}
