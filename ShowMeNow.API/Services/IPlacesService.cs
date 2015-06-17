namespace ShowMeNow.API.Services
{
    using System;
    using System.Collections.Generic;

    using Neo4jClient;

    using ShowMeNow.API.Models.RelationModeles;

    public interface IPlacesService
    {
        GraphClient InitializeNeo4J();

        void UpdateNodeProperties(int nodeId, string name);


        Person CreatePerson(Person aPerson);

        void PersonKnowsPerson(Person firstPerson, Person secondPerson);

        void PeopleKnowsPlace(Person aPerson, Place aPlace);

        List<Person> GetAllPeopleByLabel();

        List<Person> GetAPerson(string name);

        List<Person> GetAllFriends(string name);

        List<Person> GetAllPeople();

        List<Place> GetAllPlaces();

        NodeReference<Place> CreatePlace(Place aPlace);

        bool DeletePerson(string name);

        bool DeletePersonAndRelations(string name);

        void DeleteAllNodes();

    }
}
