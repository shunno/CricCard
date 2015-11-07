using Data.Repository;
using Model;
using Model.ViewModel;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;

namespace Service
{
    public interface IMatchService : IService<Match>
    {
        Match GetById(int id);

        Match GetForReview(OverDetailViewModel detailViewModel);
    }

    public class MatchService : Service<Match>, IMatchService
    {
        private readonly IRepositoryAsync<Match> _repository;

        public MatchService(IRepositoryAsync<Match> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public Match GetById(int matchId)
        {
            return _repository.GetById(matchId);
        }

        public Match GetForReview(OverDetailViewModel detailViewModel)
        {
            return _repository.GetForReview(detailViewModel);
        }

        public override void Insert(Match match)
        {
            var teamrepo = _repository.GetRepositoryAsync<Team>();
            var firstTeam = teamrepo.GetOrInsertByName(match.FirstTeamName);
            var secondTeam = teamrepo.GetOrInsertByName(match.SecondTeamName);
            match.Name = String.Format("Match Between {0} vs {1}", firstTeam.Name, secondTeam.Name);
            match.Team1 = firstTeam;
            match.Team2 = secondTeam;
            match.ObjectState = ObjectState.Added;
            _repository.InsertOrUpdateGraph(match);
        }
    }
}