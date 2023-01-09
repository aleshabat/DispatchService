
using System;
using dispatchservice.api.Domain.Maps;

namespace dispatchservice.api.Domain
{
    public class Estate : Dict
    {
        public string Type { get; set; }

        [NotMaped]
        public string FullName
        {
            get { return !string.IsNullOrEmpty(Type) ? string.Format("{0}. {1}", Type, Name) : Name; }
        }
    }
}