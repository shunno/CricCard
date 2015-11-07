using Data;
using Model;
using Repository.Pattern.Ef6;
using Service;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace WebMvc.Controllers
{
    [RoutePrefix("api/Over")]
    public class OverController : ApiController
    {
        

        private readonly IOverDetailService _overDetailService;
        private readonly IOverService _overService;

        public OverController()
        {
            var db = new ApplicationContext();
            var unitOfWork = new UnitOfWork(db);
            _overDetailService = new OverDetailService(new Repository<OverDetail>(db, unitOfWork));
            _overService= new OverService(new Repository<Over>(db, unitOfWork));


        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        public IHttpActionResult GetByMatchId(int id)
        {
            var over = new List<Over>();
            return Ok(over);
        }

        // POST api/<controller>
        public void Post(int matchId, int teamId)
        {


        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}