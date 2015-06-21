namespace ShowMeNow.API.Repositories
{
    using System.Collections.Generic;

    using Neo4jClient;

    using ShowMeNow.API.Models.RelationModeles;

    public interface IPlacesNeo4JRepository
    {
        GraphClient InitializeNeo4J();

        void UpdateNodeProperties(int nodeId, string name);

        Itinerary CreateLinkedList(Itinerary aItinerary);

        void AddNodeToLinkedList();

        List<Place> GetAPlace(string name);

        List<Place> GetAllPlaces();

        Place CreatePlace(Place aPlace);

        bool DeletePlace(string name);

        void DeleteAllNodes();

    }
}
