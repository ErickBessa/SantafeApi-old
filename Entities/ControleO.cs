using System;
using System.Collections.Generic;

#nullable disable

namespace SantafeApi.Entities
{
    public partial class ControleOs
    {
        public int Cod { get; set; }
        public int CodCliente { get; set; }
        public int CodUsuario { get; set; }
        public string Data { get; set; }
        public string DataVistoria { get; set; }

        public virtual Cliente CodClienteNavigation { get; set; }
    }
}
