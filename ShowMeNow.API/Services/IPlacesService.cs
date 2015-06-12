namespace ShowMeNow.API.Services
{
    using System.Collections.Generic;

    using Neo4jClient;

    using ShowMeNow.API.Models;

    public interface IPlacesService
    {
        GraphClient InitializeNeo4J();

        void UpdateNodeProperties(int nodeId, string name);

        void CreateInitialData();

        NodeReference<Person> CreatePerson(string name, int age, string email);

        void PeoplesKnowRelationShip(NodeReference<Person> firstPerson, NodeReference<Person> secondPerson);

        void PeoplesHatesRelationShip(NodeReference<Person> firstPerson, NodeReference<Person> secondPerson, string reason);

        List<Person> GetAllPeopleByLabel();

        List<Person> GetAllPeople();
            
            List<Person> GetAPerson(string name);

        NodeReference<Person> GetPersonNodeReference(int nodeId);
            
            List<Person> GetAllFriends(int personId);

    }
}
