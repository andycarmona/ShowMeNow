// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlacesController.cs" company="Uni-app">
//   
// </copyright>
// <summary>
//   Controller to handle places of interested
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ShowMeNow.API.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using ShowMeNow.API.Helpers;
    using ShowMeNow.API.Models.Dto;
    using ShowMeNow.API.Repositories;
    using ShowMeNow.API.Services;

    [RoutePrefix("api/Places")]
    public class PlacesController : ApiController
    {
        private readonly IEntityService _placeService;

        private readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public PlacesController()
        {
            ErrorHandler.InitializeMessageList();
            _placeService = new EntityService();
        }


        [Route("AddInitialPeople")]
        [AcceptVerbs("GET")]
        public HttpResponseMessage AddInitialPeople()
        {
            HttpResponseMessage response;
            try
            {
                response = new HttpResponseMessage(HttpStatusCode.Accepted)
                               {
                                   Content =
                                       new StringContent(
                                       "Success!! You created new people in DB")
                               };
            }
            catch (Exception e)
            {
                response = new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent(e.Message) };
                logger.Error(e.Message);
            }
            return response;
        }

        [Route("GetAllPlaces")]
        [AcceptVerbs("GET")]
        public IEnumerable GetAllPlaces()
        {
        
            IEnumerable listOfPlaces = null;
            try
            {
                listOfPlaces = _placeService.GetAllPlaces();
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return listOfPlaces;
        }

        [Route("GetAllPeople")]
        [AcceptVerbs("GET")]
        public List<PersonDto> GetAllPeople()
        {

            List<PersonDto> listOfPeople = null;
            try
            {
                listOfPeople = _placeService.GetAllPeople();
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return listOfPeople;
        }

        [Route("GetAllPeople")]
        [AcceptVerbs("GET")]
        public IEnumerable GetAllFriends(string personId)
        {
          
            IEnumerable listOfFriends = null;
            try
            {
                listOfFriends = _placeService.GetAllFriends(personId);
            }
            catch (Exception e)
            {
      
                logger.Error(e.Message);
            }

            return listOfFriends;
        }

        // GET: api/Places
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }

        // GET: api/Places/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Places
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Places/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Places/5
        public void Delete(int id)
        {
        }
    }
}
