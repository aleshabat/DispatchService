using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using dispatchservice.api.Domain;
using dispatchservice.api.Repositories;
using dispatchservice.web.Models;
using Ninject;

namespace dispatchservice.web.Controllers
{
    public class InjenerHouseController : Controller
    {

        [Inject]
        public IInjenerHouseRepository Repository { get; set; }

        [Inject]
        public IStreetRepository RepositoryStreet { get; set; }

        [Inject]
        public IRepository<Estate> RepositoryEstate { get; set; }

        [Inject]
        public IHouseRepository RepositoryHouse{ get; set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetTreeEstateByInjener(Guid estateId, Guid injenerId)
        {
            Mapper.CreateMap<Street, Tree>();
            var tree = new List<Tree>();
            using (var con = MvcApplication.Database.CreateOpenConnection())
            {
                RepositoryEstate.Connection = con;
                RepositoryStreet.Connection = con;
                RepositoryHouse.Connection = con;
                Repository.Connection = con;

                var injenerHousesId = Repository.GetList(injenerId, estateId)
                                                .Select(h => h.HouseId)
                                                .ToList();
                var streets = RepositoryStreet.GetList(estateId /*estate.Id*/)
                                                    .OrderBy( s => s.Name)
                                                    .ToList();
                    var childStreets = new List<Tree>();
                    foreach (var street in streets)
                    {
                        var houses = RepositoryHouse.GetList(street.Id, estateId)
                                                    .OrderBy(s => s.Number)
                                                    .ToList();
                        var childHouses = new List<Tree>();
                        foreach (var house in houses)
                        {
                            var itemHouse = new Tree
                            {
                                Id = house.Id,
                                Name = house.Number,
                                ParentId = street.Id,
                                Selected = injenerHousesId.Any(h => h == house.Id),
                                Level = 3,
                            };
                            childHouses.Add(itemHouse);
                        }
                        var itemStreet = new Tree
                        {
                            Id = street.Id,
                            Name = street.Name,
                            ParentId = estateId,
                            Childs = childHouses,
                            Level = 2,
                        };
                        childStreets.Add(itemStreet);
                        tree.Add(itemStreet);
                    }
            }
            var model = new TreeViewModel {Tree = tree, CheckBoxStyle = true};
            return PartialView("TreeView", model);
        }

        [HttpPost]
        public void Save(Guid injenerId, Guid estateId, string selectedHouses)
        {
            var houses = selectedHouses.Split(',').Where(h => h != "");
            using (var con = MvcApplication.Database.CreateOpenConnection())
            {
                Repository.Connection = con;

                Repository.Delete(injenerId, estateId);
                foreach (var houseId in houses)
                {
                    var injnerHouse = new InjenerHouse
                    {
                        HouseId = new Guid(houseId),
                        InjenerId = injenerId
                    };
                    Repository.Save(injnerHouse);
                }
            }
            Response.Write("<script>history.go(-1);</script>"); // вернемся на предыдущую страницу

        }
    }
}
