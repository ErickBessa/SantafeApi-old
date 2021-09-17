using System;
using System.Collections.Generic;

#nullable disable

namespace SantafeApi.Entities
{
    public partial class Bloco
    {
        public Bloco()
        {
            Locals = new HashSet<Local>();
        }

        public int CodBloco { get; set; }
        public int CodCliente { get; set; }
        public string NomeBloco { get; set; }

        public virtual Cliente CodClienteNavigation { get; set; }
        public virtual ICollection<Local> Locals { get; set; }
    }
}
