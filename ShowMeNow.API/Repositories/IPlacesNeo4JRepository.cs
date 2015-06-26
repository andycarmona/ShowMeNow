namespace ShowMeNow.API.Repositories
{
    using System;
    using System.Collections.Generic;

    using Neo4jClient;

    using ShowMeNow.API.Models.RelationModeles;

    public interface IPlacesNeo4JRepository
    {
        GraphClient InitializeNeo4J();

        void UpdatePlaceProperties(string placeId, Place placeData);

        void UpdatePlaceName(string placeId, string name);

        Itinerary CreateLinkedList(Itinerary aItinerary);

        List<Place> GetAPlace(string placeId);

        List<Place> GetAllPlaces();

        List<Place> GetPlaceByType(Place.TypeOfPlace type);

        Place CreatePlace(Place aPlace);

        bool DeletePlace(string placeId);

        void DeleteAllNodes();

    }
}
