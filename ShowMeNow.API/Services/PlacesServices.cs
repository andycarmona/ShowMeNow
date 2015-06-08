using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularJSAuthentication.API.Services
{
    using AutoMapper;

    using Neo4jClient;

    using ShowMeNow.API.Models;

    public class PlacesServices : IPlacesService
    {
        private static GraphClient _client;

        public static GraphClient Neo4JInstance()
        {
            if (_client == null)
            {
                _client = new GraphClient(new Uri("http://localhost:7474/db/data"));
                _client.Connect();
            }

            return _client;
        }

        public IEnumerable<Person> GetAllPeopleByLabel()
        {
            IEnumerable<Person> peopleByLabel = Neo4JInstance().Cypher.Match("(user:User)").Return(user => user.As<Person>()).Results;
            return peopleByLabel;
        }

        public Person GetAPerson(int personId)
        {
            //Person enumerable =
            //    Neo4JInstance()
            //        .Cypher.Match("(user:User)")
            //        .Where((Person aPerson) => aPerson.PersonId == 1234)
            //        .Return(user => user.As<Person>()).;

        }

        public List<Person> GetAllFriends(int personId)
        {
            //IEnumerable<Person> result = Neo4JInstance()
            //    .Cypher.OptionalMatch("(user:User)-[FRIENDS_WITH]-(friend:User)")
            //    .Where((Person aPerson) => aPerson.PersonId == 1234)
            //    .Return((aPerson, friend) => new { User = aPerson.As<Person>(), Friends = friend.CollectAs<Person>() })
            //    .Results;
            //return result;
        }
    }
}