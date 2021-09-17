using System;
using System.Collections.Generic;

#nullable disable

namespace SantafeApi.Entities
{
    public partial class Item
    {
        public int CodItem { get; set; }
        public string NomeItem { get; set; }
        public string Norma { get; set; }
        public int CodUsuario { get; set; }
    }
}
