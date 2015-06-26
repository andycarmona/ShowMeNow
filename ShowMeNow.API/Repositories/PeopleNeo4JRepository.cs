// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlacesServices.cs" company="Uni-app">
//   
// </copyright>
// <summary>
//   Do request against Neo4j database
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ShowMeNow.API.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Neo4jClient;

    using ShowMeNow.API.Models.RelationModeles;

    public class PeopleNeo4JRepository : IPeopleNeo4JRepository
    {

        private readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private GraphClient _neo4jClient;

        public GraphClient InitializeNeo4J()
        {
            if (this._neo4jClient != null)
            {
                return this._neo4jClient;
            }

            this._neo4jClient = new GraphClient(new Uri("http://neo4j:A5b9c0andy@localhost:7474/db/data"));
            this._neo4jClient.Connect();
            return this._neo4jClient;
        }

        /*
         *Update nodes properties , find by node id
         */
        public void UpdatePersonProperties(string personId, Person personData)
        {
            
            this._neo4jClient.Cypher
                 .Match("(aPerson:Person)")
                .Where((Person aPerson) => aPerson.PersonId == personId)
                .Set("aPerson = {properties}")
                .WithParam("properties", new Person{PersonId = personId, Age = personData.Age, Email = personData.Email, Name = personData.Name })
                .ExecuteWithoutResults();
        }

        public void UpdatePersonName(string personId, string name)
        {
            this._neo4jClient.Cypher
                .Match("(aPerson:Person)")
                .Where((Person aPerson) => aPerson.PersonId == personId)
                .Set("aPerson.Name = {name}")
                .WithParam("name", name)
                .ExecuteWithoutResults();
        }

        public void CreatePerson(Person aPerson)
        {
            var existPerson = GetAPerson(aPerson.PersonId);

            if (existPerson.Count != 0)
            {
                return;
            }

            try
            {
                this._neo4jClient.Cypher.Create("(p:Person {param})")
                    .WithParam("param", aPerson)
                    .ExecuteWithoutResults();
            }
            catch (Exception e)
            {
                this.logger.Error(e.Message);
            }
        }

        /*
         * Delete person node without relationship
         */
        public void DeletePerson(string personId)
        {

            var result
                = this.GetAllFriends(personId);
            try
            {
                if (result.Count < 1)
                {
                    this.DeleteOrphanPerson(personId);
                }
                else
                {
                    this.DeletePersonWithRelations(personId);
                }
            }
            catch (NullReferenceException)
            {
                this.DeleteOrphanPerson(personId);
            }
        }

        public bool DeleteOrphanPerson(string personId)
        {
            bool success = true;
            try
            {
                this._neo4jClient.Cypher.Match("(aPerson:Person)")
                    .Where((Person aPerson) => aPerson.PersonId == personId)
                    .Delete("aPerson")
                    .ExecuteWithoutResults();
            }
            catch (Exception e)
            {
                this.logger.Error(e.Message);
                success = false;
            }
            return success;
        }

        /*
         * Delete person node and relationship
         */
        public bool DeletePersonWithRelations(string personId)
        {
            var success = true;
            try
            {
                this._neo4jClient.Cypher.OptionalMatch("(aPerson:Person)-[r]->()")
                    .Where((Person aPerson) => aPerson.PersonId == personId)
                    .Delete("r, aPerson")
                    .ExecuteWithoutResults();
            }
            catch (Exception e)
            {
                this.logger.Error(e.Message);
                success = false;
            }
            return success;
        }
        /*
         * Creates a friendship relation between two nodes
         */
        public void PersonKnowsPerson(Person firstPerson, Person secondPerson)
        {
            try
            {
                this._neo4jClient.Cypher
                    .Match("(person1:Person)", "(person2:Person)")
                    .Where((Person person1) => person1.PersonId == firstPerson.PersonId)
                    .AndWhere((Person person2) => person2.PersonId == secondPerson.PersonId)
                    .CreateUnique("person1-[:FRIENDS_WITH]->person2")
                    .ExecuteWithoutResults();
            }
            catch (Exception e)
            {
                this.logger.Error(e.Message);
            }
        }

        public void PeopleKnowsPlace(Person aPerson, Place aPlace)
        {
            throw new NotImplementedException();
        }

        /*
         * Creates a hate relation between two nodes
         */
        public void PeoplesHatesRelationShip(NodeReference<Person> firstPerson, NodeReference<Person> secondPerson, string reason)
        {
            try
            {
                this._neo4jClient.CreateRelationship(
                    firstPerson,
                    new HatesRelationship(secondPerson, new HatesData(reason)));
            }
            catch (Exception e)
            {
                this.logger.Error(e.Message);
            }
        }
        /*
         * Get all people by label
         */
        public List<Person> GetAllPeopleByLabel()
        {
            List<Person> peopleByLabel = null;
            try
            {
                peopleByLabel =
                    this.InitializeNeo4J()
                        .Cypher.Match("(aPerson:Person)")
                        .Return(aPerson => aPerson.As<Person>())
                        .Results.ToList();
            }
            catch (Exception e)
            {
                this.logger.Error(e.Message);
            }

            return peopleByLabel;
        }
        /*
         * Get a person by personId property
         */
        public List<Person> GetAPerson(string personId)
        {
            List<Person> personList = null;

            try
            {
                personList =
                     this.InitializeNeo4J()
                         .Cypher.Match("(aPerson:Person)")
                         .Where((Person aPerson) => aPerson.PersonId == personId)
                         .Return(aPerson => aPerson.As<Person>())
                         .Results.ToList();
            }
            catch (Exception e)
            {
                this.logger.Error(e.Message);
            }

            return personList;
        }

        /*
         * Gets all people whith a friendship relation with a person
         */
        public List<Person> GetAllFriends(string personId)
        {
            List<Person> listOfFriends = null;
            try
            {
                listOfFriends = this.InitializeNeo4J()
                    .Cypher.OptionalMatch("(user:Person)-[FRIENDS_WITH]->(friend:Person)")
                    .Return((user, friend) => new { User = user.As<Person>(), Friends = friend.CollectAs<Person>() })
                    .Results.Select(result => new Person()
                                                  {
                                                      Name = result.User.Name,
                                                      Email = result.User.Email,
                                                      Age = result.User.Age,
                                                      PersonId = result.User.PersonId
                                                  }).Distinct().ToList();
            }
            catch (Exception e)
            {
                this.logger.Error(e.Message);
            }
            return listOfFriends;
        }

        public List<Person> GetAllPeople()
        {
            List<Person> personList = null;

            try
            {
                personList =
                     this.InitializeNeo4J()
                         .Cypher.Match("(aPerson:Person)")
                         .Return(aPerson => aPerson.As<Person>())
                         .Results.ToList();
            }
            catch (Exception e)
            {
                this.logger.Error(e.Message);
            }

            return personList;
        }
    }
}