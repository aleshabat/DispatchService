using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using dispatchservice.api.Domain;
using dispatchservice.api.Repositories;
using dispatchservice.web.Models;
using Ninject;

namespace dispatchservice.web.Controllers
{
    public class EstateController : Controller
    {
        [Inject]
        public IRepository<Estate> Repository { get; set; }

        public ActionResult Index()
        {
            return null;
        }

        public ActionResult FillAll(Guid? currentEstateId)
        {
            var model = new DropDownListViewModel("EstateId", "Район");

            using (var con = MvcApplication.Database.CreateOpenConnection())
            {
                Repository.Connection = con;

                var estates = Repository.All()
                                        .OrderBy(i => i.Name)
                                        .ToList();

                if (currentEstateId == null || currentEstateId == Guid.Empty)
                {
                    var firstEstate = estates.FirstOrDefault();
                    if (firstEstate != null)
                        model.ItemId = firstEstate.Id;
                }
                else
                {
                    model.ItemId = (Guid)currentEstateId;
                }

                model.Items = new SelectList(estates, "Id", "FullName", model.ItemId);
            }

            return PartialView("DropDownListPartial", model);
        }

    }
}
