using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ninject;
using dispatchservice.api.Domain;
using dispatchservice.api.Repositories;
using dispatchservice.web.Models;

namespace dispatchservice.web.Controllers
{
    public class StreetController : Controller
    {
        [Inject]
        public IStreetRepository Repository { get; set; }

        public ActionResult Index()
        {
            return null;
        }

        public ActionResult FillDropDownList(Guid estateId, Guid currentStreetId)
        {
            if (estateId == Guid.Empty)
                return null;

            var model = new DropDownListViewModel("StreetId", "Улица");
            List<Street> streets;
            using (var con = MvcApplication.Database.CreateOpenConnection())
            {
                Repository.Connection = con;
                streets = Repository.GetList(estateId).ToList();
            }
            streets = streets.OrderBy(e => e.Name).ToList();
            streets.Insert(0, new Street());

            model.ItemId = currentStreetId;
            model.Items = new SelectList(streets, "Id", "FullName", model.ItemId);

            return PartialView("DropDownListPartial", model);
        }

        public ActionResult FillSelector(Guid? estateId)
        {
            if (estateId == Guid.Empty)
                return null;

            List<Street> model;
            using (var con = MvcApplication.Database.CreateOpenConnection())
            {
                Repository.Connection = con;
                model = Repository.All().ToList();
            }
            model = model.OrderBy(s => s.Name).ToList();

            return PartialView("SelectorStreetPartialView", model);
        }
    }
}
