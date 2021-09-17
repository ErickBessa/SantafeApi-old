using System;
using System.Collections.Generic;

#nullable disable

namespace SantafeApi.Entities
{
    public partial class Status
    {
        public int CodStatus { get; set; }
        public int CodItem { get; set; }
        public string NomeStatus { get; set; }
        public string Gravidade { get; set; }

        public virtual Item CodItemNavigation { get; set; }
    }
}
