using Microsoft.AspNet.SignalR;
using Model.ViewModel;
using Repository.Pattern.UnitOfWork;
using Service;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Helper;
using WebMvc.Hubs;
using  Newtonsoft.Json;

namespace WebMvc.Controllers
{
    [EnableCors(origins: "http://localhost:2179/", headers: "*", methods: "*")]
    public class OverDetailController : ApiController
    {
        private readonly IOverDetailService _overDetailService;
        private readonly IOverService _overService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public OverDetailController(IOverDetailService overDetailService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _overDetailService = overDetailService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        public IHttpActionResult GetByMatchIdAndTeamId(int matchId, int teamId)
        {
            var overs = _overDetailService.GetlistByMathcIdAndTeamId(matchId, teamId);
            return Ok(overs);
        }

        [HttpGet]
        [Route("api/OverDetail/GetByLastBall")]
        public IHttpActionResult GetByLastBall([FromUri]OverDetailViewModel detailViewModel)
        {
            var overs = _overDetailService.GetlistByBallNumber(detailViewModel);
            if (detailViewModel.Description.IsNotNullOrEmpty())
            {
                overs.RemoveAt(0);
            }
            return Ok(overs);
        }

        // POST api/<controller>
        //

        //public async Task<IHttpActionResult> Post()
        public async Task<IHttpActionResult> Post(OverDetailViewModel detailViewModel)
        {
            try
            {
                var overdetail = _overDetailService.MakeRandomBall(detailViewModel.MatchID, detailViewModel.TeamID);
                
                await _unitOfWorkAsync.SaveChangesAsync();
                var json = JsonConvert.SerializeObject(overdetail);
                var ctx = GlobalHost.ConnectionManager.GetHubContext<MatchHub>();
                var connectionIds = MatchHub._mapping[detailViewModel.MatchID];

                if (connectionIds != null && connectionIds.Any())
                {
                    ctx.Clients.Clients(connectionIds).makeball(json);
                }
                return Ok();
            }
            catch (Exception ex)
            {
            }
            return BadRequest("Can't Bowl At This Moment");
        }
    }
}