namespace ShowMeNow.API.Services
{
    using System.Collections.Generic;

    using Neo4jClient;

    using ShowMeNow.API.Models;

    public interface IPlacesService
    {
        GraphClient InitializeNeo4J();

        void CreateInitialData();

        void CreatePerson(string name, int age, string email);

        void PeoplesKnowRelationShip(NodeReference<Person> firstPerson, NodeReference<Person> secondPerson);

        void PeoplesHatesRelationShip(NodeReference<Person> firstPerson, NodeReference<Person> secondPerson, string reason);

        List<Person> GetAllPeopleByLabel();

        List<Person> GetAPerson(int personId);

        List<Person> GetAllFriends(int personId);

    }
}
