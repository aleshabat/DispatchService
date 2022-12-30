using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using dispatchservice.api.Domain;

namespace dispatchservice.web.Models.Ticket
{
    public class TicketAddEditViewModel
    {
        public Guid TicketId { get; set; }

        //[RangeAttribute(typeof(String), "{00000000-0000-0000-0000-000000000001}", "{ffffffff-ffff-ffff-ffff-ffffffffffff}", ErrorMessage = "Выберите район")]
        public Guid ServiceId { get; set; }
        [Display(Name = "Услуга")]
        public IEnumerable<SelectListItem> Services { get; set; }

        public IEnumerable<SelectListItem> ServiceIdPrices { get; set; }

        //[Required(ErrorMessage = "Выберите район")]
        public Guid EstateId { get; set; }
        [Display(Name = "Район")]
        public IEnumerable<SelectListItem> Estates { get; set; }

        //[Required(ErrorMessage = "Выберите улицу")]
        public Guid StreetId { get; set; }

        //[Display(Name = "Улица")]
       // public IEnumerable<SelectListItem> Streets { get; set; }

        //[Required(ErrorMessage = "Выберите дом")]
        public Guid HouseId { get; set; }

        //[Display(Name = "Дом")]
        //public IEnumerable<SelectListItem> Houses { get; set; }

        [Display(Name = "Поиск дома")]
        public string SearchString { get; set; }
        /*public string Service { get; set; }
        public string Estate { get; set; }
        public string Street { get; set; }
        public string House { get; set; }*/

        [Display(Name = "Квартира")]
        [Required(ErrorMessage = "Введите номер квартиры")]
        public string Flat { get; set; }

        [Display(Name = "Дата заявки")]
        [Required(ErrorMessage = "Введите дату заявки")]
        public string DateTime { get; set; }

        [Display(Name = "Дата исполения")]
        public string DateExecute { get; set; }

        [Display(Name = "Дата отказа")]
        public string DateCancel { get; set; }

        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Display(Name = "Мобильный телефон")]
        public string MobilePhone { get; set; }

        [Display(Name = "Стоимость")]
        [Required(ErrorMessage = "Введите стоимость")]
        [RegularExpression("[0-9]*([\\,\\.][0-9]{2})?", ErrorMessage = "Некорректная сумма")]
        public decimal Price { get; set; }

        public Guid InjenerId { get; set; }

        //public bool AllowDeleted { get; set; }

        //[Display(Name = "Специалист")]
        //public IEnumerable<SelectListItem> Injeners { get; set; }

        //public string Injener { get; set; }
        //public bool IsCanceled { get; set; }
        //public bool IsExecuted { get; set; }
    }
}