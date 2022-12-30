using System;
using System.ComponentModel.DataAnnotations;

namespace dispatchservice.web.Models.CustomerDict
{
    public class ServiceViewModal
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Введите услугу")]
        [Display(Name = "Услуга")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите цену")]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Display(Name = "Удалить")]
        public bool Deleted { get; set; }
    }
}