using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using dispatchservice.api.Domain;
using dispatchservice.api.Repositories;

namespace dispatchservice.api.Services
{
    public class DictService<T> where T: Entity
    {
        public IDbConnection Connection { get; set; }
        private ITicketRepository _ticketRepository;
        private IInjenerHouseRepository _injenerHouseRepository;
        private IRepository<T> _repository;

        public DictService(IDbConnection connection)
        {
            Connection = connection;
            _ticketRepository = new TicketRepository { Connection = Connection };
            _injenerHouseRepository = new InjenerHouseRepository { Connection = Connection };
            _repository = new Repository<T> { Connection = Connection };
        }

        public void Delete(Guid id)
        {
            bool allowDeleted = true;
            var dictName = typeof(T).Name;
            switch (dictName)
            {
                case "Injener":
                    var hasTicket = _ticketRepository.GetList(null, null, id, null).Any();
                    var hasInjenerHouse = _injenerHouseRepository.GetList(id, null).Any();
                    var injenerRep = new Repository<Injener> { Connection = Connection };

                    allowDeleted = !(hasTicket || hasInjenerHouse);
                    if (!allowDeleted)
                    {
                        var injener = injenerRep.Get(id);
                        injener.Deleted = true;

                        injenerRep.SaveOrUpdate(injener);
                    }
                    //else
                    //{
                    //    injenerRep.Delete(id);
                    //}
                    break;
                case "Service":
                    allowDeleted = !_ticketRepository.GetList(null, id, null, null).Any();
                    if (!allowDeleted)
                    {
                        var serviceRep = new Repository<Service>{ Connection = Connection };
                        var sevice = serviceRep.Get(id);
                        sevice.Deleted = true;

                        serviceRep.SaveOrUpdate(sevice);
                    }
                    break;
            }
            if (allowDeleted)
                _repository.Delete(id);
        }

        public void CancelDelete(Guid id)
        {
            var dictName = typeof(T).Name;
            switch (dictName)
            {
                case "Injener":
                    var injenerRep = new Repository<Injener> { Connection = Connection };
                    var injener = injenerRep.Get(id);
                    injener.Deleted = false;

                    injenerRep.SaveOrUpdate(injener);
                    break;
                case "Service":
                    var serviceRep = new Repository<Service> { Connection = Connection };
                    var sevice = serviceRep.Get(id);
                    sevice.Deleted = false;

                    serviceRep.SaveOrUpdate(sevice);
                    break;
            }
        }
    }
}
