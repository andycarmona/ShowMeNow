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

        private readonly log4net.ILog logger =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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

        public void UpdatePlaceProperties(Guid placeId, Place placeData)
        {
            this._neo4jClient.Cypher
                .Match("(aPlace:Place)")
                .Where((Place aPlace) => aPlace.PlaceId == placeId)
                .Set("aPlace = {properties}")
                .WithParam(
                    "properties",
                    new Place
                        {
                            PlaceId = placeId,
                            Address = placeData.Address,
                            Coordinates = placeData.Coordinates,
                            EMail = placeData.EMail,
                            FeedbackId = placeData.FeedbackId,
                            Name = placeData.Name,
                            ParentName = placeData.ParentName,
                            Telephone = placeData.Telephone,
                            Type = placeData.Type
                        })
                .ExecuteWithoutResults();
        }

        public void UpdatePlaceName(Guid placeId, string name)
        {
            this._neo4jClient.Cypher
                .Match("(aPlace:Place)")
                .Where((Place aPlace) => aPlace.PlaceId == placeId)
                .Set("aPlace.Name = {placeId}")
                .WithParam("placeId", name)
                .ExecuteWithoutResults();
        }

    
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

        public List<Place> GetPlaceByType(Place.TypeOfPlace type)
        {
            List<Place> placeList = null;

            try
            {
                placeList =
                    this.InitializeNeo4J()
                        .Cypher.Match("(aPlace:Place)")
                        .Where((Place aPlace) => aPlace.Type == type)
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
                    this._neo4jClient.Cypher.Create("(root:Place {param})-[:LINK]->(root)")
                        .WithParam("param", aItinerary)
                        .Return<Itinerary>("root")
                        .Results.Single();
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
                refPlace =
                    this._neo4jClient.Cypher.Create("(p:Place {param})")
                        .WithParam("param", aPlace)
                        .Return<Place>("p")
                        .Results.Single();
            }
            catch (Exception e)
            {
                this.logger.Error(e.Message);
            }

            return refPlace;
        }

        public bool DeletePlace(Guid placeId)
        {
            bool success = true;
            try
            {
                this._neo4jClient.Cypher.Match("(aPlace:Place)")
                    .Where((Place aPlace) => aPlace.PlaceId == placeId)
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


        public void DeleteAllNodes()
        {
            throw new NotImplementedException();
        }

        public List<Place> GetAPlace(Guid placeId)
        {
            List<Place> placeList = null;

            try
            {
                placeList =
                    this.InitializeNeo4J()
                        .Cypher.Match("(aPlace:Place)")
                        .Where((Place aPlace) => aPlace.PlaceId == placeId)
                        .Return(aPlace => aPlace.As<Place>())
                        .Results.ToList();
            }
            catch (Exception e)
            {
                this.logger.Error(e.Message);
            }

            return placeList;
        }
    }
}