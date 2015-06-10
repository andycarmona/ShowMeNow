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
        private GraphClient _neo4jClient;
        private readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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

        public void CreatePerson(string name, int age, string email)
        {
            try
            {
                NodeReference<Person> refPerson =
                    this._neo4jClient.Create(new Person() { Age = age, Email = email, Name = name, PersonId = 0 });
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }
        }

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

        public List<Person> GetAllPeopleByLabel()
        {
            List<Person> peopleByLabel = null;
            try
            {
                peopleByLabel =
                    this.InitializeNeo4J()
                        .Cypher.Match("(user:User)")
                        .Return(user => user.As<Person>())
                        .Results.ToList();
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return peopleByLabel;
        }

        public List<Person> GetAPerson(int personId)
        {
            List<Person> personList = null;
            
            try
            {
               personList =
                    this.InitializeNeo4J()
                        .Cypher.Match("(user:User)")
                        .Where((Person aPerson) => aPerson.PersonId == 1234)
                        .Return(user => user.As<Person>())
                        .Results.ToList();
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return personList;
        }

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