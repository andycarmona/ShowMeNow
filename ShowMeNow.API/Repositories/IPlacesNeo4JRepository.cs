namespace ShowMeNow.API.Repositories
{
    using System;
    using System.Collections.Generic;

    using Neo4jClient;

    using ShowMeNow.API.Models.RelationModeles;

    public interface IPlacesNeo4JRepository
    {
        GraphClient InitializeNeo4J();

        void UpdatePlaceProperties(Guid placeId, Place placeData);

        void UpdatePlaceName(Guid placeId, string name);

        Itinerary CreateLinkedList(Itinerary aItinerary);

        List<Place> GetAPlace(Guid placeId);

        List<Place> GetAllPlaces();

        List<Place> GetPlaceByType(Place.TypeOfPlace type);

        Place CreatePlace(Place aPlace);

        bool DeletePlace(Guid placeId);

        void DeleteAllNodes();

    }
}
