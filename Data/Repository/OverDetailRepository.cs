using Model;
using Model.ViewModel;
using Repository.Pattern.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Data.Repository
{
    public static class OverDetailRepository
    {
        public static int GetTotalByMatchIDAndTeamID(this IRepositoryAsync<OverDetail> repository, int matchId, int teamId)
        {
            int result = 0;
            var overRepo = repository.GetRepositoryAsync<Over>();
            var overDetails = repository.Queryable();
            var overs = overRepo.Queryable();

            result = (from over in overs
                      join overdetail in overDetails on over.OverID equals overdetail.OverID
                      where over.MatchID == matchId && over.TeamID == teamId
                      select overdetail.RunTaken
                ).DefaultIfEmpty(0)
                .Sum();
            return result;
        }

        public static int GetTotalByBallNumber(this IRepositoryAsync<OverDetail> repository, int matchId, int teamId,int index)
        {
            int result = 0;
            var overRepo = repository.GetRepositoryAsync<Over>();
            var overDetails = repository.Queryable();
            var overs = overRepo.Queryable();

            result = (from over in overs
                      join overdetail in overDetails on over.OverID equals overdetail.OverID
                      where over.MatchID == matchId && over.TeamID == teamId && overdetail.BallIndex<= index
                      select overdetail.RunTaken
                ).DefaultIfEmpty(0)
                .Sum();
            return result;
        }

        public static OverDetail GetLastBallByMatchIdAndTeamId(this IRepositoryAsync<OverDetail> repository, int matchId, int teamId)
        {
            OverDetail overDetail = null;
            var overRepo = repository.GetRepositoryAsync<Over>();
            var overDetails = repository.Queryable();
            var overs = overRepo.Queryable();
            var result = (from over in overs
                          join overdetail in overDetails on over.OverID equals overdetail.OverID
                          where over.MatchID == matchId && over.TeamID == teamId
                          orderby overdetail.BallIndex descending
                          select new { overdetail, over }
                ).FirstOrDefault();
            if (result != null)
            {
                overDetail = result.overdetail;
                overDetail.OverNumber = result.over.OverNumber;
            }
            return overDetail;
        }

        public static OverDetail GetLastBallByBallNumber(this IRepositoryAsync<OverDetail> repository,
            int matchId, int teamId,int overNumber,int ballNumber)
        {
            var overRepo = repository.GetRepositoryAsync<Over>();
            var overDetails = repository.Queryable();
            var overs = overRepo.Queryable();
            var result = (from over in overs
                          join overdetail in overDetails on over.OverID equals overdetail.OverID
                          where over.MatchID == matchId && over.TeamID == teamId && over.OverNumber==overNumber && overdetail.BallNumber==ballNumber
                          orderby overdetail.BallIndex descending
                          select overdetail
                ).FirstOrDefault();
            return result;
        }

        public static List<OverDetailViewModel> GetListByMatchIdAndTeamIdAndOverNumAndBallNum(this IRepositoryAsync<OverDetail> repository, 
            int matchId, int teamId,int index)
        {
            var list = new List<OverDetailViewModel>();
            var overRepo = repository.GetRepositoryAsync<Over>();
            var overDetails = repository.Queryable();
            var overs = overRepo.Queryable();

            list = (from over in overs
                    join overdetail in overDetails on over.OverID equals overdetail.OverID
                    where over.MatchID == matchId && over.TeamID == teamId && overdetail.BallIndex<=index
                    orderby overdetail.BallIndex descending
                    select new
                    {
                        MatchID = over.MatchID,
                        TeamID = over.TeamID,
                        OverNumber = over.OverNumber,
                        BallNumber = overdetail.BallNumber,
                        RunTaken = overdetail.RunTaken,
                        Description = overdetail.Description
                    }).Take(12).AsEnumerable().Select(item => new OverDetailViewModel
                    {
                        MatchID = item.MatchID,
                        TeamID = item.TeamID,
                        OverNumber = item.OverNumber,
                        BallNumber = item.BallNumber,
                        RunTaken = item.RunTaken,
                        Description = item.Description
                    }).ToList();
            return list;
        }

        public static List<OverDetailViewModel> GetListByMatchIdAndTeamId(this IRepositoryAsync<OverDetail> repository,
            int matchId, int teamId)
        {
            var list = new List<OverDetailViewModel>();
            var overRepo = repository.GetRepositoryAsync<Over>();
            var overDetails = repository.Queryable();
            var overs = overRepo.Queryable();
            list = (from over in overs
                    join overdetail in overDetails on over.OverID equals overdetail.OverID
                    where over.MatchID == matchId && over.TeamID == teamId
                    orderby overdetail.BallIndex descending
                    select new
                    {
                        MatchID = over.MatchID,
                        TeamID = over.TeamID,
                        OverNumber = over.OverNumber,
                        BallNumber = overdetail.BallNumber,
                        RunTaken = overdetail.RunTaken,
                        Description = overdetail.Description
                    }).AsEnumerable().Select(item => new OverDetailViewModel
                    {
                        MatchID = item.MatchID,
                        TeamID = item.TeamID,
                        OverNumber = item.OverNumber,
                        BallNumber = item.BallNumber,
                        RunTaken = item.RunTaken,
                        Description = item.Description
                    }).Take(12).ToList();
            return list;
        }
    }
}