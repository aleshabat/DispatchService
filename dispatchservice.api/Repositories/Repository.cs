using System;
using System.Collections.Generic;
using System.IO;
using dispatchservice.api.Domain;
using System.Data;
using DapperExtensions;

namespace dispatchservice.api.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        public IDbConnection Connection { get; set; }

        public void Save(T obj)
        {
            Connection.Insert(obj);
        }

        public void Update(T obj)
        {
            Connection.Update(obj);
        }

        public void SaveOrUpdate(T obj)
        {
            if (obj.IsNew())
            {
                GenerateNewId(obj);
                Save(obj);
            }
            else
                Update(obj);
        }

        public void Delete(T obj)
        {
            Connection.Delete(obj);
        }

        public void Delete(object id)
        {
            Delete(Get(id));
        }

        public void GenerateNewId(T obj)
        {
            var pid = obj.GetType().GetProperty("Id");

            var type = pid.PropertyType;

            if (type == typeof(Guid))
            {
                pid.SetValue(obj, Guid.NewGuid(), null);
            }
            else
            {
                throw new InvalidDataException();
            }
        }

        public virtual IEnumerable<T> All()
        {
            return Connection.GetList<T>();
        }

        public virtual T Get(object id)
        {
            return Connection.Get<T>(id);
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public IList<T> Select<TKey>(int strartWith, int countItem, Func<T, TKey> order)
        {
            throw new NotImplementedException();
        }
    }
}
