using System;
using System.Collections.Generic;

namespace dispatchservice.web.Models.CustomerDict
{
    public class CustomerDictViewModel
    {
        public List<DictItem> Items { get; set; }
        public string Name { get; set; }
        public object Model { get; set; }
    }

    public class DictItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public bool? Deleted { get; set; }
    }
}