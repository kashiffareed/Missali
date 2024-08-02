using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Data.HandsDB;

namespace Hands.Service
{
    public class ServiceBase<TEntity> : IService<TEntity> where TEntity : class, IEntity

    {

        protected readonly HandsDBContext _db;

        public ServiceBase()
        {
            _db = new HandsDBContext();
        }



        public ServiceBase(HandsDBContext db)
        {
            _db = db;
        }


        public TEntity GetById(int id)
        {
            
            //return _db.DbSet<TEntity>.Find(id);
            return _db.Set<TEntity>().Find(id);

        }


        public IEnumerable<TEntity> GetAll()
        {
            return _db.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> GetAllActive()
        {
            return _db.Set<TEntity>().Where(e => e.IsActive).ToList();
        }



        public void Insert(TEntity model)
        {
            _db.Set<TEntity>().Add(model);
        } 


        public void Update(TEntity model)
        {
            _db.Set<TEntity>().AddOrUpdate(model);
        }

        public void Remove(TEntity model)
        {
            _db.Set<TEntity>().Remove(model);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        //public string GetNameById(int mwraUnionCouncilId)
        //{
        //    throw new NotImplementedException();
        //}
      

        public void InsertRange(List<TEntity> model)
        {
            _db.Set<TEntity>().AddRange(model);
        }
    }
}
