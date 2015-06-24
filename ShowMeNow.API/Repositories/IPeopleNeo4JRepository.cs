namespace ShowMeNow.API.Repositories
{
    using System;
    using System.Collections.Generic;

    using Neo4jClient;

    using ShowMeNow.API.Models.RelationModeles;

    public interface IPeopleNeo4JRepository
    {
        GraphClient InitializeNeo4J();

        void UpdatePersonProperties(Guid personId, Person aPerson);

        void UpdatePersonName(Guid personId, string name);

        void CreatePerson(Person aPerson);

        void PersonKnowsPerson(Person firstPerson, Person secondPerson);

        void PeopleKnowsPlace(Person aPerson, Place aPlace);

        List<Person> GetAllPeopleByLabel();

        List<Person> GetAPerson(Guid personId);

        List<Person> GetAllFriends(Guid personId);

        List<Person> GetAllPeople();

        void DeletePerson(Guid personId);

        bool DeletePersonWithRelations(Guid personId);

        bool DeleteOrphanPerson(Guid personId);

        void DeleteAllNodes();

    }
}
