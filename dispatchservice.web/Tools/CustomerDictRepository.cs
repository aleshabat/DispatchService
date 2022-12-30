using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using dispatchservice.api.Domain;
using dispatchservice.api.Repositories;
using dispatchservice.api.Services;
using dispatchservice.web;
using dispatchservice.web.Models.CustomerDict;
using dispatchservice.web.Tools;

namespace prod.web.Tools
{
    public class CustomerDictRepository<T> : ICustomerDictRepository<T> where T : Entity
    {
        private readonly IRepository<T> _dictRepository;

        public CustomerDictRepository(IRepository<T> dictRepository)
        {
            _dictRepository = dictRepository;
        }

        public T GetData(Guid id)
        {
            using (var con = MvcApplication.Database.CreateOpenConnection())
            {
                _dictRepository.Connection = con;
                return _dictRepository.Get(id);
            }
        }

        public List<DictItem> Select()
        {
            throw new NotImplementedException();
        }

        public int CountTotalItemCount()
        {
            using (var con = MvcApplication.Database.CreateOpenConnection())
            {
                _dictRepository.Connection = con;
                return _dictRepository.Count();
            }
        }

        public void Delete(Guid id)
        {
            using (var con = MvcApplication.Database.CreateOpenConnection())
            {
                var dictService = new DictService<T>(con);
                dictService.Delete(id);

                //_dictRepository.Connection = con;
                //_dictRepository.Delete(id);
            }
        }

        public void CancelDelete(Guid id)
        {
            using (var con = MvcApplication.Database.CreateOpenConnection())
            {
                var dictService = new DictService<T>(con);
                dictService.CancelDelete(id);
            }
        }

        //todo переименовать в Save
        public void Insert(T item)
        {
            using (var con = MvcApplication.Database.CreateOpenConnection())
            {
                _dictRepository.Connection = con;
                _dictRepository.SaveOrUpdate(item);
            }
        }

        //todo переименовать в Update
        public void Save(T item)
        {
            using (var con = MvcApplication.Database.CreateOpenConnection())
            {
                _dictRepository.Connection = con;
                _dictRepository.Update(item);
            }
        }

        public List<DictItem> PagedList(int pageNumber, int pageSize)
        {
            using (var con = MvcApplication.Database.CreateOpenConnection())
            {
                _dictRepository.Connection = con;
                var list = (List<T>) _dictRepository.Select<T>(pageNumber*pageSize, pageSize, null);
                Mapper.CreateMap<T, DictItem>();
                return Mapper.Map<List<T>, List<DictItem>>(list);
            }
        }

        public List<DictItem> All()
        {
            using (var con = MvcApplication.Database.CreateOpenConnection())
            {
                _dictRepository.Connection = con;
                var list = _dictRepository.All().ToList();
                Mapper.CreateMap<T, DictItem>();
                return Mapper.Map<List<T>, List<DictItem>>(list);
            }
        }
    }
}