using System;
using System.Collections.Generic;

#nullable disable

namespace SantafeApi.Entities
{
    public partial class Local
    {
        public int CodLocal { get; set; }
        public int CodCliente { get; set; }
        public string NomeLocal { get; set; }
        public int CodBloco { get; set; }
        public string NomeBloco { get; set; }

        public virtual Bloco CodBlocoNavigation { get; set; }
    }
}
