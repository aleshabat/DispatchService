using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AutoMapper;
using dispatchservice.api.Domain;
using dispatchservice.api.Repositories;
using dispatchservice.api.Services;
using dispatchservice.web.Models;
using Ninject;

namespace dispatchservice.web.Controllers
{
    public class HouseController : Controller
    {
        [Inject]
        public IHouseRepository Repository { get; set; }

        [Inject]
        public IStreetRepository RepositoryStreet { get; set; }

        [Inject]
        public IRepository<Estate> RepositoryEstate { get; set; }

        public ActionResult Index(Guid? estateId)
        {
            return View(estateId);
        }

        public ActionResult FillDropDownList(Guid streetId, Guid estateId, Guid currentHouseId)
        {
            if (streetId == Guid.Empty)
                return null;

            var model = new DropDownListViewModel("HouseId", "Дом");
            List<House> houses;
            using (var con = MvcApplication.Database.CreateOpenConnection())
            {
                Repository.Connection = con;
                houses = Repository.GetList(streetId, estateId).ToList();
            }
            houses.Add(new House());
            houses = houses.OrderBy(e => e.Number).ToList();
            model.ItemId = currentHouseId;
            model.Items = new SelectList(houses, "Id", "Number", model.ItemId);

            return PartialView("DropDownListPartial", model);
        }

        public ActionResult GetTreeEstate(Guid estateId)
        {
            Mapper.CreateMap<Street, Tree>();
            var tree = new List<Tree>();
            
            using (var con = MvcApplication.Database.CreateOpenConnection())
            {
                RepositoryEstate.Connection = con;
                RepositoryStreet.Connection = con;
                Repository.Connection = con;

                var streets = RepositoryStreet.GetList(estateId)
                                                    .OrderBy(s => s.Name)
                                                    .ToList();

                var childStreets = new List<Tree>();
                foreach (var street in streets)
                {
                    var houses = Repository.GetList(street.Id, estateId)
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
                            Deleted = house.Deleted,
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
            var model = new TreeViewModel { Tree = tree, CheckBoxStyle = false };
            return PartialView("TreeView", model);
        }

        [HttpPost]
        public ActionResult Save(Guid estateId, string jsonTree)
        {
            var serializer = new JavaScriptSerializer();
            var tree = serializer.Deserialize<IEnumerable<JsTree>>(jsonTree);

            foreach (var item in tree)
            {
                item.a_attr.id = item.a_attr.id.Replace("_anchor", "");
                item.text = item.text.Replace("\\n", "").Trim();
            }

            using (var con = MvcApplication.Database.CreateOpenConnection())
            {
                RepositoryEstate.Connection = con;
                RepositoryStreet.Connection = con;
                Repository.Connection = con;

                var streetsFromDB = RepositoryStreet.GetList(estateId);

                //Получим все дома по району
                var housesFromDB = new List<House>();
                foreach (var street in streetsFromDB)
                    housesFromDB.AddRange(Repository.GetList(street.Id, estateId));

                var streetsFromTree = tree.Where(t => t.parent == "#" || string.IsNullOrEmpty(t.parent)).ToList();
                var housesFromTree = tree.Where(t => t.parent != "#" && !string.IsNullOrEmpty(t.parent)).ToList();

                var streetService = new StreetService(con);


                // Добавляем улицы
                foreach (var street in streetsFromTree)
                {
                    //if (!streetsFromDB.Any(t => t.Id.ToString() ==  street.a_attr.id))
                    {
                        // Добавляем дома
                        var housesByStreetFromTree = tree.Where(t => t.parent == street.id).ToList();
                        foreach (var house in housesByStreetFromTree)
                        {
                            if (!housesFromDB.Any(h => h.Id.ToString() == house.a_attr.id))
                            {
                                var newHouse = new House
                                {
                                    Number = house.text,
                                    EstateId = estateId,
                                    StreetId = new Guid(street.a_attr.id),
                                    Deleted = false
                                };

                                Repository.Save(newHouse);
                            }
                        }
                    }
                }

                //Удаляем или обновляем дома
                foreach (var house in housesFromDB)
                {
                    var fh = housesFromTree.FirstOrDefault(t => t.a_attr.id == house.Id.ToString());
                    if (fh == null)
                        streetService.DeleteHouse(house);
                    else
                    {
                        if (house.Number != fh.text || house.Deleted != fh.a_attr.deleted)
                        {
                            house.Number = fh.text;
                            house.Deleted = fh.a_attr.deleted;
                            Repository.SaveOrUpdate(house);
                        }
                    }
                }

            }
            return RedirectToAction("Index", new {estateId = estateId});
        }


        class JsTreeAttribute
        {
            public string id { get; set; }
            public bool deleted { get; set; }
        }

        class JsTree
        {
            public string id { get; set; }
            public string parent { get; set; }
            public string text { get; set; }
            public JsTreeAttribute a_attr { get; set; }
        }

    }
}
