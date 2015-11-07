using Data.Repository;
using Model;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;

namespace Service
{
    public interface IOverService : IService<Over>
    {
        Over Getbymatchid(int id);
    }

    public class OverService : Service<Over>, IOverService
    {
        public static string[] descriptions = new string[]{
             "1 Run Taken" ,
           "2 Run Taken"  ,
           "3 Run Taken"  ,
           "4 Run Taken"  ,
           "5 Run Taken"  ,
           "6 Run Taken"  ,
           "Wide is Givn"
};

        private readonly IRepositoryAsync<Over> _repository;

        public OverService(IRepositoryAsync<Over> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public Over Getbymatchid(int overId)
        {
            return _repository.GetById(overId);
        }


        public Over GetRandomOverDetails(int matchID,int teamID,int overnumber) {

            Over over = new Over();
            Random rand = new Random();

            int index = rand.Next(0,6);
            over.MatchID = matchID;
            over.TeamID = teamID;
            over.OverNumber = overnumber;
            return over;
        }



    }
}