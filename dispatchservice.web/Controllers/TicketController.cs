using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using dispatchservice.api.Domain;
using dispatchservice.api.Repositories;
using dispatchservice.api.Services;
using dispatchservice.web.Models;
using dispatchservice.web.Models.Ticket;
using Ninject;

namespace dispatchservice.web.Controllers
{
    public class TicketController : Controller
    {
        [Inject]
        public ITicketRepository Repository { get; set; }
        [Inject]
        public IRepository<Estate> RepositoryEstate { get; set; }
        [Inject]
        public IRepository<Service> RepositoryService { get; set; }
        [Inject]
        public IInjenerHouseRepository RepositoryInjenerHouse { get; set; }

        public ActionResult Index()
        {
            var model = new IndexViewModel
            {
                DateStart = DateTime.Now.ToShortDateString(),
                DateEnd = DateTime.Now.ToShortDateString(),
            };
            return View("Index", model);
        }

        public ActionResult Fill(string dateStart, string dateEnd)
        {
            Mapper.CreateMap<Ticket, TicketViewModel>()
                .ForMember(dest => dest.TicketId, opt => opt.MapFrom(source => source.Id))
                .ForMember(dest => dest.DateTime, opt => opt.MapFrom(source => source.DateTime.ToShortDateString()))
                .ForMember(dest => dest.AddressHouse, opt => opt.MapFrom(source => source.House.FullAddress ))
                .ForMember(dest => dest.Injener, opt => opt.MapFrom(source => source.Injener.Name))
                .ForMember(dest => dest.Service, opt => opt.MapFrom(source => source.Service.Name))
                .ForMember(dest => dest.IsCanceled, opt => opt.MapFrom(source => source.DateCancel != null))
                .ForMember(dest => dest.IsExecuted, opt => opt.MapFrom(source => source.DateExecute != null))
                .ForMember(dest => dest.AllowDeleted, opt => opt.MapFrom(source => source.DateCancel != null && source.DateExecute != null));
            
            List<Ticket> tickets;
            using(var con = MvcApplication.Database.CreateOpenConnection())
            {
                Repository.Connection = con;
                tickets = Repository.GetList(Convert.ToDateTime(dateStart),
                                             Convert.ToDateTime(dateEnd).AddDays(1),
                                             null
                                            ).ToList();
            }
            var model = Mapper.Map<List<TicketViewModel>>(tickets);

            return PartialView("Ticket/TicketListPartial", model);
        }

        public ActionResult Add()
        {
            var model = new TicketAddEditViewModel();
            
            List<Estate> estates;
            List<Service> services;
            using (var con = MvcApplication.Database.CreateOpenConnection())
            {
                RepositoryEstate.Connection = con;
                RepositoryService.Connection = con;
                estates = RepositoryEstate.All().ToList();
                services = RepositoryService.All().ToList();
            }
            estates = estates.OrderBy(e => e.Name).ToList();
            estates.Insert(0, new Estate());

            model.Estates = new SelectList(estates, "Id", "Name", Guid.Empty);
            model.Services = new SelectList(services, "Id", "Name", model.ServiceId);
            model.ServiceIdPrices = new SelectList(services, "Id", "Price");
            model.DateTime = DateTime.Now.ToShortDateString();

            ViewBag.Title = "Новая заявка";
            return View("AddEdit", model);
        }

        public ActionResult Edit(Guid id)
        {
            var model = new TicketAddEditViewModel();
            Ticket ticket;
            List<Service> services;
            List<Estate> estates;

            Mapper.CreateMap<Ticket, TicketAddEditViewModel>()
                .ForMember(dest => dest.TicketId, opt => opt.MapFrom(source => source.Id))
                .ForMember(dest => dest.DateTime, opt => opt.MapFrom(source => source.DateTime.ToShortDateString()))
                .ForMember(dest => dest.DateCancel, opt => opt.MapFrom(source => source.DateCancel != null? source.DateCancel.Value.ToShortDateString() : ""))
                .ForMember(dest => dest.DateExecute, opt => opt.MapFrom(source => source.DateExecute != null ? source.DateExecute.Value.ToShortDateString() : ""))
                .ForMember(dest => dest.HouseId, opt => opt.MapFrom(source => source.House.Id))
                .ForMember(dest => dest.StreetId, opt => opt.MapFrom(source => source.House.Street.Id))
                .ForMember(dest => dest.EstateId, opt => opt.MapFrom(source => source.House.Estate.Id))
                .ForMember(dest => dest.InjenerId, opt => opt.MapFrom(source => source.Injener.Id))
                .ForMember(dest => dest.ServiceId, opt => opt.MapFrom(source => source.Service.Id))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(source => source.Price));

            using (var con = MvcApplication.Database.CreateOpenConnection())
            {
                Repository.Connection = con;
                RepositoryService.Connection = con;
                RepositoryEstate.Connection = con;

                ticket = Repository.Get(id);
                services = RepositoryService.All().ToList();
                estates = RepositoryEstate.All().ToList();
            }
            estates = estates.OrderBy(e => e.Name).ToList();


            model = Mapper.Map<TicketAddEditViewModel>(ticket);
            model.Services = new SelectList(services, "Id", "Name", model.ServiceId);
            model.Estates = new SelectList(estates, "Id", "Name", model.EstateId);
            model.ServiceIdPrices = new SelectList(services, "Id", "Price", null);

            return View("AddEdit", model);
        }

        public ActionResult Delete(Guid id)
        {
            var model = new TicketAddEditViewModel();
            return RedirectToAction("Index");
        }

        public ActionResult FillDateExecute(Guid serviceId, Guid houseId)
        {
            DateTime? dateExecute;
            using (var con = MvcApplication.Database.CreateOpenConnection())
            {
                var ticketService = new TicketService(con);
                dateExecute = ticketService.GetDateExecuteForNewTicket(serviceId, houseId);
            }

            var model = new InputViewModel("DateExecute", "Дата исполнения",
                                            dateExecute != null? dateExecute.Value.ToShortDateString(): "",
                                            InputViewModel.InputType.DatePicker
                                          );

            return PartialView("InputPartial", model);
        }

        public ActionResult Save(TicketAddEditViewModel model)
        {
            Mapper.CreateMap<TicketAddEditViewModel, Ticket>()
                .ForMember(dest => dest.Id,
                            opt => opt.MapFrom(source => source.TicketId))
                .ForMember(dest => dest.DateTime,
                            opt => opt.MapFrom(source => Convert.ToDateTime(source.DateTime)))
                .ForMember(dest => dest.DateCancel,
                            opt => opt.MapFrom(source => !string.IsNullOrEmpty(source.DateCancel)? (DateTime?)Convert.ToDateTime(source.DateCancel) : null))
                .ForMember(dest => dest.DateExecute,
                            opt => opt.MapFrom(source => !string.IsNullOrEmpty(source.DateExecute) ? (DateTime?)Convert.ToDateTime(source.DateExecute) : null));

            var ticket = Mapper.Map<Ticket>(model);
            using (var con = MvcApplication.Database.CreateOpenConnection())
            {
                RepositoryInjenerHouse.Connection = con;
                Repository.Connection = con;
                Repository.SaveOrUpdate(ticket);
            }

            return RedirectToAction("Index");
        }
    }
}
