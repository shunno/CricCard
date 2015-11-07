using Model;
using Model.ViewModel;
using Repository.Pattern.UnitOfWork;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebMvc.Controllers
{
    [EnableCors(origins: "http://localhost:2179/", headers: "*", methods: "*")]
    public class MatchController : ApiController
    {
        private readonly IMatchService _matchService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public MatchController(IMatchService matchService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _matchService = matchService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        // GET api/<controller>
        public IEnumerable<Match> Get()
        {
            return _matchService.Queryable().ToList();
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                var match = _matchService.GetById(id);
                return Ok(match);
            }
            catch (Exception ex)
            {
                return BadRequest("Match Not Found!!");
            }
        }

        [HttpGet]
        [Route("api/Match/GetForReview")]
        public IHttpActionResult GetForReview([FromUri]OverDetailViewModel detailViewModel)
        {
            try
            {
                var match = _matchService.GetForReview(detailViewModel);
                return Ok(match);
            }
            catch (Exception ex)
            {
                return BadRequest("Match Not Found!!");
            }
        }

        // POST api/<controller>
        public async Task<IHttpActionResult> Post(Match match)
        {
            try
            {
                _matchService.Insert(match);
                await _unitOfWorkAsync.SaveChangesAsync();
                return Ok(match.MatchID);
            }
            catch (Exception ex)
            {
                return BadRequest("Can't Create Match");
            }
        }
    }
}