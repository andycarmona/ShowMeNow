namespace ShowMeNow.API.Repositories
{
    using System;
    using System.Collections.Generic;

    using Neo4jClient;

    using ShowMeNow.API.Models.RelationModeles;

    public interface IPeopleNeo4JRepository
    {
        GraphClient InitializeNeo4J();

        void UpdatePersonProperties(string personId, Person aPerson);

        void UpdatePersonName(string personId, string name);

        void CreatePerson(Person aPerson);

        void PersonKnowsPerson(Person firstPerson, Person secondPerson);

        void PeopleKnowsPlace(Person aPerson, Place aPlace);

        List<Person> GetAllPeopleByLabel();

        List<Person> GetAPerson(string personId);

        List<Person> GetAllFriends(string personId);

        List<Person> GetAllPeople();

        void DeletePerson(string personId);

        bool DeletePersonWithRelations(string personId);

        bool DeleteOrphanPerson(string personId);
    }
}
