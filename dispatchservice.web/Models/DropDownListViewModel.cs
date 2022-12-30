using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace dispatchservice.web.Models
{
    public class DropDownListViewModel : HtmlElement
    {
        public DropDownListViewModel(){}
        public DropDownListViewModel(string name, string caption)
        {
            Name = name;
            Caption = caption;
        }

        public Guid ItemId { get; set; }
        public IEnumerable<SelectListItem> Items { get; set; }

        //public bool Hiden { get; set; }
    }
}