using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace dispatchservice.web.Models.Ticket
{
    public class IndexViewModel
    {
        [Display(Name = "Дата с")]
        //[Required(ErrorMessage = "Укажите дату")]
        public string DateStart { get; set; }

        [Display(Name = "Дата по")]
        //[Required(ErrorMessage = "Укажите дату")]
        public string DateEnd { get; set; }
    }
}