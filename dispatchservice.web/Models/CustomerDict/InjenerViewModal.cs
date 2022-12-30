using System;
using System.ComponentModel.DataAnnotations;

namespace dispatchservice.web.Models.CustomerDict
{
    public class InjenerViewModal
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Введите ФИО")]
        [Display(Name = "ФИО")]
        public string Name { get; set; }

        [Display(Name = "Удалить")]
        public bool Deleted { get; set; }
    }
}