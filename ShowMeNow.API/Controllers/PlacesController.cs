namespace ShowMeNow.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using Neo4jClient;

    using ShowMeNow.API.Helpers;
    using ShowMeNow.API.Services;

    [RoutePrefix("api/Places")]
    public class PlacesController : ApiController
    {
        private readonly IPlacesService _placeService;
        private readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public PlacesController()
        {
            ErrorHandler.InitializeMessageList();
            _placeService = new PlacesServices();
        }

        public PlacesController(IPlacesService placeService)
        {
            _placeService = placeService;
        }

        // GET: api/Places
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [AllowAnonymous]
        [Route("InitializeDatabase")]
        [AcceptVerbs("GET")]
        public HttpResponseMessage InitializeDatabase()
        {
            
            HttpResponseMessage response = null;
            try
            {
                _placeService.InitializeNeo4J();
                response = new HttpResponseMessage(HttpStatusCode.Accepted);
                response.Content = new StringContent("Success!! You are connected to Neo4j database");
            }
            catch (Exception e)
            {
                response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                response.Content = new StringContent(e.Message);
                logger.Error(e.Message);
            }
            return response;
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
