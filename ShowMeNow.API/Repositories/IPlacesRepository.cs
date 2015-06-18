namespace ShowMeNow.API.Repositories
{
    using System.Collections.Generic;

    using Neo4jClient;

    using ShowMeNow.API.Models.RelationModeles;

    public interface IPlacesRepository
    {
        GraphClient InitializeNeo4J();

        void UpdateNodeProperties(int nodeId, string name);


        Person CreatePerson(Person aPerson);

        void PersonKnowsPerson(Person firstPerson, Person secondPerson);

        void PeopleKnowsPlace(Person aPerson, Place aPlace);

        List<Person> GetAllPeopleByLabel();

        List<Person> GetAPerson(string name);

        List<Place> GetAPlace(string name);

        List<Person> GetAllFriends(string name);

        List<Person> GetAllPeople();

        List<Place> GetAllPlaces();

        Place CreatePlace(Place aPlace);



        void DeletePerson(string name);

        bool DeletePlace(string name);

        bool DeletePersonWithRelations(string name);

        bool DeleteOrphanPerson(string name);

        void DeleteAllNodes();

    }
}
