namespace ShowMeNow.API.Repositories
{
    using System.Collections.Generic;

    using Neo4jClient;

    using ShowMeNow.API.Models.RelationModeles;

    public interface IPeopleNeo4JRepository
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

        void DeletePerson(string name);

        bool DeletePersonWithRelations(string name);

        bool DeleteOrphanPerson(string name);

        void DeleteAllNodes();

    }
}
