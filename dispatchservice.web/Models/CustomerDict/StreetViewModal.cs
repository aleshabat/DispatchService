using System;
using System.ComponentModel.DataAnnotations;

namespace dispatchservice.web.Models.CustomerDict
{
    public class StreetViewModal
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Введите улицу")]
        [Display(Name = "Улица")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите тип")]
        [Display(Name = "Тип")]
        public string Type { get; set; }
    }
}