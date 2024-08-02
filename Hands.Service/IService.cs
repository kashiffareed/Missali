using System.Collections.Generic;
using Hands.Data.HandsDB;

namespace Hands.Service
{
    public interface IService<TEntity> where TEntity : class
    {

        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
        void Insert(TEntity model);
        void Update(TEntity model);
        void Remove(TEntity model);
        void SaveChanges();
        //HandsDBContext<TEntity> Uow { get; }
        IEnumerable<TEntity> GetAllActive();
        //  string GetNameById(int mwraUnionCouncilId);
        void InsertRange(List<TEntity> model);
    }


}
