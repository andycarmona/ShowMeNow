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
    using ShowMeNow.API.Services;

    public class PlacesNeo4JRepository : IPlacesNeo4JRepository
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

        public void AddNodeToLinkedList()
        {
            var refNewNode;
            try
            {
                refNewNode =
                    this._neo4jClient.Cypher
                    .Match("(root)-[:LINK*0..]->(before),(after)-[:LINK*0..]->(root),(before)-[old:LINK]->(after)")
                    .Where((Itinerary root) => root.Name == "ROOT")
                    .AndWhere((Itinerary before) => before.ItineraryId == "ROOT")
                    .Create("(p:Person {param})")
                        .WithParam("param", aPerson)
                        .Return<Person>("p")
                        .Results.Single();
            }
            catch (Exception e)
            {
                this.logger.Error(e.Message);
            }
     
WHERE root.name = 'ROOT' AND (before.value < 25 OR before = root) AND (25 < after.value OR after =
  root)
CREATE UNIQUE (before)-[:LINK]->({ value:25 })-[:LINK]->(after)
DELETE old
        }

        public Person CreatePerson(Person aPerson)
        {
            Person refPerson = null;
            var existPerson = GetAPerson(aPerson.Name);

            if (existPerson.Count >= 1)
            {
                return null;
            }

            try
            {
                refPerson =
                    this._neo4jClient.Cypher.Create("(p:Person {param})")
                        .WithParam("param", aPerson)
                        .Return<Person>("p")
                        .Results.Single();
            }
            catch (Exception e)
            {
                this.logger.Error(e.Message);
            }

            return refPerson;
        }

        /*
         * Creates a Node places
         */

        public List<Place> GetAllPlaces()
        {
            List<Place> placeList = null;

            try
            {
                placeList =
                     this.InitializeNeo4J()
                         .Cypher.Match("(aPlace:Place)")
                         .Return(aPlace => aPlace.As<Place>())
                         .Results.ToList();
            }
            catch (Exception e)
            {
                this.logger.Error(e.Message);
            }

            return placeList;
        }

        public Itinerary CreateLinkedList(Itinerary aItinerary)
        {
            Itinerary refItinerary = null;
            try
            {
                refItinerary =
                    this._neo4jClient.Cypher
                    .Create("(root:Place {param})-[:LINK]->(root)")
                        .WithParam("param", aItinerary)
                         .Return<Itinerary>("root")
                        .Results
                        .Single();
            }
            catch (Exception e)
            {
                this.logger.Error(e.Message);

            }
            return refItinerary;
        }



        public Place CreatePlace(Place aPlace)
        {
            Place refPlace = null;
            try
            {
                refPlace = this._neo4jClient.Cypher
                .Create("(p:Place {param})")
                .WithParam("param", aPlace)
                .Return<Place>("p")
                 .Results
                .Single();
            }
            catch (Exception e)
            {
                this.logger.Error(e.Message);
            }

            return refPlace;
        }

        /*
         * Delete person node without relationship
         */
        public void DeletePerson(string name)
        {

            var result
                = this.GetAllFriends(name);
            try
            {
                if (result.Count < 1)
                {
                    this.DeleteOrphanPerson(name);
                }
                else
                {
                    this.DeletePersonWithRelations(name);
                }
            }
            catch (NullReferenceException)
            {
                this.DeleteOrphanPerson(name);
            }
        }

        public bool DeletePlace(string name)
        {
            bool success = true;
            try
            {
                this._neo4jClient.Cypher.Match("(aPlace:Place)")
                    .Where((Place aPlace) => aPlace.Name == name)
                    .Delete("aPlace")
                    .ExecuteWithoutResults();
            }
            catch (Exception e)
            {
                this.logger.Error(e.Message);
                success = false;
            }
            return success;
        }

        public bool DeleteOrphanPerson(string name)
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
                this.logger.Error(e.Message);
                success = false;
            }
            return success;
        }

        public void DeleteAllNodes()
        {
            throw new NotImplementedException();
        }



        /*
         * Delete person node and relationship
         */
        public bool DeletePersonWithRelations(string name)
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
                    .Where((Person person1) => person1.Name == firstPerson.Name)
                    .AndWhere((Person person2) => person2.Name == secondPerson.Name)
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
                this.logger.Error(e.Message);
            }

            return personList;
        }

        public List<Place> GetAPlace(string name)
        {
            List<Place> placeList = null;

            try
            {
                placeList =
                     this.InitializeNeo4J()
                         .Cypher.Match("(aPlace:Place)")
                         .Where((Place aPlace) => aPlace.Name == name)
                         .Return(aPlace => aPlace.As<Place>())
                         .Results.ToList();
            }
            catch (Exception e)
            {
                this.logger.Error(e.Message);
            }

            return placeList;
        }
        /*
         * Gets all people whith a friendship relation with a person
         */
        public List<Person> GetAllFriends(string name)
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