using System;
using System.Collections.Generic;
using System.Data;
using dispatchservice.api.Domain;

namespace dispatchservice.api.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        IDbConnection Connection { get; set; }

        void Save(T obj);

        void Update(T obj);

        void SaveOrUpdate(T obj);

        void Delete(T obj);

        void Delete(object id);

        void GenerateNewId(T obj);

        IEnumerable<T> All();

        T Get(object id);

        int Count();

        /// <summary>
        /// Постраничная выборка
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"> </typeparam>
        /// <param name="strartWith">с какого элемента выводить</param>
        /// <param name="countItem">количество элементов на странице</param>
        /// <param name="order">условие сортировки</param>
        /// <returns></returns>
        IList<T> Select<TKey>(int strartWith, int countItem, Func<T, TKey> order);



    }
}
