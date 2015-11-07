using Data.Repository;
using Model;
using Model.ViewModel;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;

namespace Service
{
    public interface IOverDetailService : IService<OverDetail>
    {
        OverDetailViewModel MakeRandomBall(int matchId, int teamId);
        List<OverDetailViewModel> GetlistByMathcIdAndTeamId(int matchId, int teamId);

        List<OverDetailViewModel> GetlistByBallNumber(OverDetailViewModel detailViewModel);
    }

    public class OverDetailService : Service<OverDetail>, IOverDetailService
    {
        private static string[] descriptions = new string[]
        {
            "0 Run Taken"
           ,"1 Run Taken"
           ,"2 Run Taken"
           ,"3 Run Taken"
           ,"4 Run Scored !!"
           ,"5 Run Taken !!"
           ,"6 Run Scored !!"
           ,"Wide is Given"
        };

        private readonly IRepositoryAsync<OverDetail> _repository;

        public OverDetailService(IRepositoryAsync<OverDetail> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public OverDetailViewModel MakeRandomBall(int matchId, int teamId)
        {
            Over over = null;
            OverDetail overDetail = null;
            int ballnumber = 0;
            int overnumber = 0;
            var lastBall = _repository.GetLastBallByMatchIdAndTeamId(matchId, teamId);
            if (lastBall == null || lastBall.BallNumber == 6)
            {
                overnumber = lastBall != null ? lastBall.OverNumber+1 : 1;
                var overRepo = _repository.GetRepositoryAsync<Over>();
                over = new Over() { MatchID = matchId, TeamID = teamId, OverNumber = overnumber };
                overRepo.Insert(over);
            }
            else
            {
                over = new Over() { OverID = lastBall.OverID };
                overnumber = lastBall.OverNumber;
                ballnumber = lastBall.BallNumber;
            }
            overDetail = GetRandomOverDetailDetail(over.OverID, ballnumber);
            overDetail.BallIndex= lastBall != null ? lastBall.BallIndex + 1 : 1;
            _repository.Insert(overDetail);


            OverDetailViewModel detailViewModel= new OverDetailViewModel();
            detailViewModel.MatchID = matchId;
            detailViewModel.TeamID = teamId;
            detailViewModel.OverNumber = overnumber;
            detailViewModel.BallNumber = overDetail.BallNumber;
            detailViewModel.RunTaken = overDetail.RunTaken;
            detailViewModel.Description = overDetail.Description;
            detailViewModel.TotalRun = _repository.GetTotalByBallNumber(matchId,teamId,overDetail.BallIndex)+ detailViewModel.RunTaken;
            return detailViewModel;
        }

        #region Helper

        private OverDetail GetRandomOverDetailDetail(int overID, int ballnumber)
        {
            OverDetail overdetail = new OverDetail();
            Random rand = new Random();
            int index = rand.Next(0, 7);
            string description = descriptions[index];
            overdetail.OverID = overID;
            overdetail.Description = description;
            if (index==7)//to Check that current ball is wide
            {
                overdetail.IsWide = true;
                overdetail.RunTaken = 1;
                overdetail.BallNumber = overdetail.BallNumber;
            }
            else
            {
                overdetail.BallNumber = ballnumber + 1;
                overdetail.RunTaken = index;
            }
            return overdetail;
        }

        #endregion Helper

        public List<OverDetailViewModel> GetlistByMathcIdAndTeamId(int matchId, int teamId)
        {
            return _repository.GetListByMatchIdAndTeamId(matchId, teamId);
        }

        public List<OverDetailViewModel> GetlistByBallNumber(OverDetailViewModel detailViewModel)
        {
            var lastBall = _repository.GetLastBallByBallNumber(detailViewModel.MatchID,detailViewModel.TeamID,detailViewModel.OverNumber,detailViewModel.BallNumber);
            if (lastBall==null)
            {
                throw  new Exception();
            }
            return _repository.GetListByMatchIdAndTeamIdAndOverNumAndBallNum(detailViewModel.MatchID, detailViewModel.TeamID, lastBall.BallIndex);
        }
    }
}