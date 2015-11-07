using Model;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repository;
using Model.ViewModel;

namespace Service
{

    public interface IDoctorService : IService<DoctorProfile>
    {
        List<DoctorProfile> GetDoctors(DoctorSearchViewModel searchViewModel);
        DoctorProfile GetProfileByUserId(string userId);

    }
    public class DoctorService : Service<DoctorProfile>, IDoctorService
    {

        private readonly IRepositoryAsync<DoctorProfile> _repository;

        public DoctorService(IRepositoryAsync<DoctorProfile> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public List<DoctorProfile> GetDoctors(DoctorSearchViewModel searchViewModel)
        {
            return _repository.Search(searchViewModel);
        }

        public DoctorProfile GetProfileByUserId(string userId)
        {
            return _repository.GetByUserId(userId);
        }

        #region Radius
        #endregion
    }
}
