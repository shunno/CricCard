using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Query;
using Model;
using Model.ViewModel;
using Repository.Pattern.Repositories;

namespace Data.Repository
{
    public static class MatchRepository
    {

        public static Match GetById(this IRepositoryAsync<Match> repository, int MatchId)
        {
            var matches = repository.Queryable();
            var overDetailsrepo = repository.GetRepositoryAsync<OverDetail>();
            var teams = repository.GetRepository<Team>().Queryable();
            var singlematch = (from m in matches join team1 in teams
                              on m.Team1ID equals team1.TeamID
                               join team2 in teams on m.Team2ID equals team2.TeamID
                               where m.MatchID == MatchId
                               select new { m, team1, team2 }).AsEnumerable()
                              .Select(item => new Match
                              {
                                  MatchID=item.m.MatchID,
                                  FirstTeamName=item.team1.Name,
                                  SecondTeamName = item.team2.Name,
                                  Team1ID = item.m.Team1ID,
                                  Team2ID = item.m.Team2ID,
                                  IsTeam1Bowl=item.m.IsTeam1Bowl,
                                  IsTeam2Bowl = item.m.IsTeam2Bowl
                              }).FirstOrDefault();

            if (singlematch !=null)
            {
                singlematch.TotalRun = overDetailsrepo.GetTotalByMatchIDAndTeamID(singlematch.MatchID,
                (singlematch.IsTeam1Bowl ? singlematch.Team1ID : singlematch.Team2ID));
                
            }
            return singlematch;
        }
        /// <summary>
        /// This Method will be Used For Review
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="detailViewModel"></param>
        /// <returns></returns>
        public static Match GetForReview(this IRepositoryAsync<Match> repository, OverDetailViewModel detailViewModel)
        {
            var matches = repository.Queryable();
            var overDetailsrepo = repository.GetRepositoryAsync<OverDetail>();
            var teams = repository.GetRepository<Team>().Queryable();

            var singlematch = (from m in matches
                               join team1 in teams
            on m.Team1ID equals team1.TeamID
                               join team2 in teams on m.Team2ID equals team2.TeamID
                               where m.MatchID == detailViewModel.MatchID
                               select new { m, team1, team2 }).AsEnumerable()
                              .Select(item => new Match
                              {
                                  MatchID = item.m.MatchID,
                                  FirstTeamName = item.team1.Name,
                                  SecondTeamName = item.team2.Name,
                                  Team1ID = item.m.Team1ID,
                                  Team2ID = item.m.Team2ID,
                                  IsTeam1Bowl = item.m.IsTeam1Bowl,
                                  IsTeam2Bowl = item.m.IsTeam2Bowl
                              }).FirstOrDefault();

            if (singlematch != null)
            {
                var lastBall = overDetailsrepo.GetLastBallByBallNumber(detailViewModel.MatchID,detailViewModel.TeamID,detailViewModel.OverNumber,detailViewModel.BallNumber);
                if (lastBall !=null)
                {
                    singlematch.TotalRun = overDetailsrepo.GetTotalByBallNumber(detailViewModel.MatchID,
                        detailViewModel.TeamID, lastBall.BallIndex);
                }
            }
            return singlematch;
        }

        

        public static Match GetByIdAndTeamIdAndBallNumber(this IRepositoryAsync<Match> repository, int MatchId)
        {
            var matches = repository.Queryable();
            var overDetailsrepo = repository.GetRepositoryAsync<OverDetail>();
            var teams = repository.GetRepository<Team>().Queryable();

            var singlematch = (from m in matches
                               join team1 in teams
            on m.Team1ID equals team1.TeamID
                               join team2 in teams on m.Team2ID equals team2.TeamID
                               where m.MatchID == MatchId
                               select new { m, team1, team2 }).AsEnumerable()
                              .Select(item => new Match
                              {
                                  MatchID = item.m.MatchID,
                                  FirstTeamName = item.team1.Name,
                                  SecondTeamName = item.team2.Name,
                                  Team1ID = item.m.Team1ID,
                                  Team2ID = item.m.Team2ID,
                                  IsTeam1Bowl = item.m.IsTeam1Bowl,
                                  IsTeam2Bowl = item.m.IsTeam2Bowl
                              }).FirstOrDefault();

            if (singlematch != null)
            {
                singlematch.TotalRun = overDetailsrepo.GetTotalByMatchIDAndTeamID(singlematch.MatchID,
                (singlematch.IsTeam1Bowl ? singlematch.Team1ID : singlematch.Team2ID));
            }
            return singlematch;
        }
    }
}
