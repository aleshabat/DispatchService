using System;
using System.Collections.Generic;
using dispatchservice.api.Domain;
using dispatchservice.web.Models.CustomerDict;

namespace dispatchservice.web.Tools
{
    public interface ICustomerDictRepository<T> where T : Entity
    {
        T GetData(Guid id);
        List<DictItem> Select();
        List<DictItem> PagedList(int pageNumber, int pageSize);
        int CountTotalItemCount();
        void Delete(Guid id);
        void CancelDelete(Guid id);
        void Insert(T item);
        void Save(T item);
        List<DictItem> All();
    }
}