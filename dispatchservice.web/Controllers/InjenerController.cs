using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using dispatchservice.api.Services;
using dispatchservice.web.Models.CustomerDict;
using Ninject;
using dispatchservice.api.Domain;
using dispatchservice.api.Repositories;
using dispatchservice.web.Models;

namespace dispatchservice.web.Controllers
{
    public class InjenerController : Controller
    {
        [Inject]
        public IInjenerHouseRepository InjenerHouseRepository { get; set; }

        [Inject]
        public IRepository<Injener> Repository { get; set; }

        [Inject]
        public ITicketRepository TicketRepository { get; set; }

        public ActionResult Index()
        {
            var model = new CustomerDictViewModel();
            model.Name = "Injener";
            model.Model = new InjenerViewModal();

            List<Injener> injeners = null;
            using (var con = MvcApplication.Database.CreateOpenConnection())
            {   Repository.Connection = con;
                injeners = Repository.All().ToList();

            }
            Mapper.CreateMap<Injener, DictItem>();
            model.Items = Mapper.Map<List<Injener>, List<DictItem>>(injeners);

            return View("DictIndex", model);
        }

        public ActionResult FillAll(bool withDeleted)
        {
            var model = new DropDownListViewModel("InjenerId", "Специалист");

            using (var con = MvcApplication.Database.CreateOpenConnection())
            {
                Repository.Connection = con;

                var injeners = Repository.All()
                                        .Where(i => (i.Deleted == withDeleted || withDeleted))
                                        .OrderBy( i => i.Name)
                                        .ToList();
                var firstinjener = injeners.FirstOrDefault();
                if (firstinjener != null)
                    model.ItemId = firstinjener.Id;
                model.Items = new SelectList(injeners, "Id", "Name", model.ItemId);
            }

            return PartialView("DropDownListPartial", model);
        }

        public ActionResult Fill(Guid serviceId, Guid houseId, string dateExecute, Guid currentInjenerId)
        {
            var model = new DropDownListViewModel("InjenerId", "Специалист");

            List<Injener> injeners = null;
            using (var con = MvcApplication.Database.CreateOpenConnection())
            {
                InjenerHouseRepository.Connection = con;
                injeners = InjenerHouseRepository.GetList(houseId)
                                                .Select(i => i.Injener).ToList();

                if (currentInjenerId == Guid.Empty)
                {
                    var ticketService = new TicketService(con);
                    Injener injenerForNewTicket = null;
                    if(!string.IsNullOrEmpty(dateExecute))
                        injenerForNewTicket = ticketService.GetInjenerForNewTicket(serviceId, houseId, Convert.ToDateTime(dateExecute));
                    
                    if (injenerForNewTicket != null)
                        currentInjenerId = injenerForNewTicket.Id;
                    else
                        injeners.Insert(0, new Injener());//Добавим пустую строку DropDownList
                }


            }
            //currentInjenerId = currentInjenerId != Guid.Empty ? currentInjenerId : injeners.First().Id;
            model.ItemId = currentInjenerId;
            model.Items = new SelectList(injeners, "Id", "Name", model.ItemId);

            return PartialView("DropDownListPartial", model);
        }

        public ActionResult Details(Guid id)
        {
            Injener item;
            using (var con = MvcApplication.Database.CreateOpenConnection())
            {
                Repository.Connection = con;
                item = Repository.Get(id);
            }
            return Json(item, JsonRequestBehavior.AllowGet);
        }
    }
}
