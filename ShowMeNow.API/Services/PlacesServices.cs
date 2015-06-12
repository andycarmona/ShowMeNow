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

    using ShowMeNow.API.Models;
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
         * Creates initial graph in DB
         * */
        public void CreateInitialData()
        {
            // Create entities
            var refA = this.CreatePerson("Person A", 12, "test@er.se");
            var refB = this.CreatePerson("Person B", 13, "test@er.se");
            var refC = this.CreatePerson("Person C", 32, "test@hotmail.se");
            var refD = this.CreatePerson("Person D", 42, "test@gmail.se");

            // Create relationships
            PeoplesKnowRelationShip(refA, refB);
            PeoplesKnowRelationShip(refB, refC);
            PeoplesHatesRelationShip(refB, refD, "crazy guy");
            PeoplesHatesRelationShip(refC, refD, "Don't know why");
            PeoplesKnowRelationShip(refD, refA);
        }

        /*
         * Creates a node Person in graph
         */
        public NodeReference<Person> CreatePerson(string name, int age, string email)
        {
            NodeReference<Person> refPerson = null;
            try
            {
                refPerson = this._neo4jClient.Create(new Person { Age = age, Email = email, Name = name, PersonId = Guid.NewGuid() });
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return refPerson;
        }

        /*
         * Creates a friendship relation between two nodes
         */
        public void PeoplesKnowRelationShip(NodeReference<Person> firstPerson, NodeReference<Person> secondPerson)
        {
            try
            {
                this._neo4jClient.CreateRelationship(firstPerson, new KnowsRelationship(secondPerson));
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }
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
                        .Cypher.Match("(user:Person)")
                        .Return(user => user.As<Person>())
                        .Results.ToList();
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return peopleByLabel;
        }

        /*
         * Gets all people with certain relation with a person
         */
        public List<Person> GetAllPeople()
        {
            var query = this.InitializeNeo4J()
             .Cypher
                .Start(new { root = this.InitializeNeo4J().RootNode })
                     .Match("root-[:HATES]->person")
                        .Return(user => user.As<Person>()).Results;
            return query.ToList();
         
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
                         .Cypher.Match("(user:User)")
                         .Where((Person aPerson) => aPerson.Name == name)
                         .Return(user => user.As<Person>())
                         .Results.ToList();
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return personList;
        }

        /*
         * Gets Person node reference in graph by id
         */
        public NodeReference<Person> GetPersonNodeReference(int nodeId)
        {
            return (NodeReference<Person>)nodeId;
        }

        /*
         * Gets all people whith a friendship relation with a person
         */
        public List<Person> GetAllFriends(int personId)
        {
            List<Person> listOfFriends = null;
            //try
            //{
            //    listOfFriends =
            //        this.InitializeNeo4J()
            //            .Cypher.OptionalMatch("(user:User)-[FRIENDS_WITH]-(friend:User)")
            //            .Where((Person aPerson) => aPerson.PersonId == 1234)
            //            .Return(
            //                (aPerson, friend) =>
            //                new { User = aPerson.As<Person>(), Friends = friend.CollectAs<Person>() })
            //            .Results.ToList();
            //}
            //catch (Exception e)
            //{
            //    logger.Error(e.Message);
            //}

            return listOfFriends;
        }
    }
}