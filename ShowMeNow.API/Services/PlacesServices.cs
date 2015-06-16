// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlacesServices.cs" company="Uni-app">
//   
// </copyright>
// <summary>
//   Do request against Neo4j database
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ShowMeNow.API.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Neo4jClient;

    using ShowMeNow.API.Models.RelationModeles;

    public class PlacesServices : IPlacesService
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
        public void UpdateNodeProperties(int nodeId, string name)
        {
            var nodeReference = (NodeReference<Person>)nodeId;
            this._neo4jClient.Update(
                nodeReference,
                node =>
                {
                    node.Name = name;
                });
        }


        /*
         * Creates a node Person in graph
         */
        public Person CreatePerson(Person aPerson)
        {
            Person refPerson = null;
            try
            {
                refPerson = this._neo4jClient.Cypher
                .Create("(p:Person {param})")
                .WithParam("param", aPerson)
                .Return<Person>("p")
                 .Results
                .Single();
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return refPerson;
        }

        /*
         * Creates a Node places
         */
        public NodeReference<Place> CreatePlace(Place aPlace)
        {
            NodeReference<Place> refPlaces = null;
            try
            {
                refPlaces = this._neo4jClient.Create(aPlace);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return refPlaces;
        }

        public bool DeletePerson(string name)
        {
            bool success = true;
            try
            {
                this._neo4jClient.Cypher.Match("(aPerson:Person)")
                    .Where((Person aPerson) => aPerson.Name == name)
                    .Delete("aPerson")
                    .ExecuteWithoutResults();
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                success = false;
            }
            return success;
        }

        public void DeleteAllNodes()
        {
            throw new NotImplementedException();
        }

        public bool DeletePersonAndRelations(string name)
        {
            var success = true;
            try
            {
                this._neo4jClient.Cypher.OptionalMatch("(aPerson:Person)-[r]->()")
                    .Where((Person aPerson) => aPerson.Name == name)
                    .Delete("r, aPerson")
                    .ExecuteWithoutResults();
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
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
                    .Where((Person person1) => person1.Name == firstPerson.Name)
                    .AndWhere((Person person2) => person2.Name == secondPerson.Name)
                    .CreateUnique("person1-[:FRIENDS_WITH]->person2")
                    .ExecuteWithoutResults();
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
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
                logger.Error(e.Message);
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
                logger.Error(e.Message);
            }

            return peopleByLabel;
        }

        /*
         * Get a person by personId property
         */
        public List<Person> GetAPerson(string name)
        {
            List<Person> personList = null;

            try
            {
                personList =
                     this.InitializeNeo4J()
                         .Cypher.Match("(aPerson:Person)")
                         .Where((Person aPerson) => aPerson.Name == name)
                         .Return(aPerson => aPerson.As<Person>())
                         .Results.ToList();
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return personList;
        }

        /*
         * Gets all people whith a friendship relation with a person
         */
        public List<Person> GetAllFriends(string name)
        {
            List<Person> listOfFriends = null;
            try
            {
                listOfFriends =
                    this.InitializeNeo4J()
                        .Cypher.Match("(aPerson:Person)-[:FRIENDS_WITH]->()")
                        .Where((Person aPerson) => aPerson.Name == name)
                        .Return(aPerson => aPerson.As<Person>())
                        .Results.ToList();
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return listOfFriends;
        }

    }
}