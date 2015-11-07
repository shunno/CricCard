using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Query;
using Model;
using Model.ViewModel;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Repositories;

namespace Data.Repository
{
    public static class TeamRepository
    {

        public static Team GetOrInsertByName(this IRepositoryAsync<Team> repository, string name)
        {
            
            var teams = repository.Queryable();

            var singleteam = teams.FirstOrDefault(item => item.Name.ToLower() == name.ToLower());
            if (singleteam == null)
            {
                singleteam = new Team() {Name = name};
                singleteam.ObjectState=ObjectState.Added;
                //repository.Insert(singleteam);
            }
            return singleteam;


        }
    }
}
