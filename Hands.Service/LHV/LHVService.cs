using System.Collections.Generic;
using Hands.Service.AppUser;

namespace Hands.Service.LHV
{
    public class LHVService : ServiceBase<Data.HandsDB.AppUser>, ILHVService
    {
        private IAppUserService _appUserService;

        public LHVService()
        {
            _appUserService = new AppUserService();
        }

        public IEnumerable<Data.HandsDB.AppUser> GetAll(int projectid)
        {
            return _appUserService.GetAll("lhv",projectid);
        }

        public IEnumerable<Data.HandsDB.AppUser> GetAllActive(int projectid)
        {
            return _appUserService.GetAllActive("lhv", projectid);
        }


        public int GetCount()
        {
            return _appUserService.GetCount("lhv");
        }


        public void Update(Data.HandsDB.AppUser existingModel)
        {
            _appUserService.Update(existingModel);
        }

        public void Insert(Data.HandsDB.AppUser appUser)
        {
            _appUserService.Insert(appUser);
        }


        

        public Data.HandsDB.AppUser GetById(int id)
        {
            return _appUserService.GetById(id);
        }
        public void SaveChanges()
        {
            _appUserService.SaveChanges();
        }

        
    }
}

