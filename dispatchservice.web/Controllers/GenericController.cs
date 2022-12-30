using System;
using System.Web.Mvc;
using dispatchservice.api.Domain;
using dispatchservice.web.Models.CustomerDict;
using dispatchservice.web.Tools;

namespace dispatchservice.web.Controllers
{

    public class GenericController<T> : Controller where T : Entity
    {
        private readonly object _viewModel;
        private readonly ICustomerDictRepository<T> _genericRepository;

        public GenericController(object viewModel, ICustomerDictRepository<T> genericRepository)
        {
            _viewModel = viewModel;
            _genericRepository = genericRepository;
        }

        public ActionResult Index(int? page)
        {
            var items = _genericRepository.All();
            var model = new CustomerDictViewModel { Items = items, Name = typeof(T).Name, Model = _viewModel };
            return View("Index", model);

        }

        public void Delete(Guid id)
        {
            _genericRepository.Delete(id);

        }

        public void CancelDelete(Guid id)
        {
            _genericRepository.CancelDelete(id);

        }

        public void Insert(T model)
        {
            _genericRepository.Insert(model);
        }

        public void Save(T model)
        {
            _genericRepository.Save(model);
        }

        public ActionResult Details(Guid id)
        {
            var item = _genericRepository.GetData(id);
            return Json(item, JsonRequestBehavior.AllowGet);
        }

    }
}
