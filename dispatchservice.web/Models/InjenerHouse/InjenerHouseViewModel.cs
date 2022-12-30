using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace dispatchservice.web.Models.InjenerHouse
{
    public class InjenerHouseViewModel
    {
        public Guid InjenerId { get; set; }
        [Display(Name = "Специалист")]
        public IEnumerable<SelectListItem> Injeners { get; set; }

        public Guid EstateId { get; set; }
        [Display(Name = "Район")]
        public IEnumerable<SelectListItem> Estates { get; set; }
    }
}