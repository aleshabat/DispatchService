using System;
using System.ComponentModel.DataAnnotations;

namespace dispatchservice.web.Models.CustomerDict
{
    public class EstateViewModal
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Введите район")]
        [Display(Name = "Район")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите тип")]
        [Display(Name = "Тип")]
        public string Type { get; set; }
    }
}