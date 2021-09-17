using System;
using System.Collections.Generic;

#nullable disable

namespace SantafeApi.Entities
{
    public partial class Vistoria
    {
        public int Cod { get; set; }
        public int CodControle { get; set; }
        public int CodCliente { get; set; }
        public string NomeCliente { get; set; }
        public string TipoLocal { get; set; }
        public int CodBloco { get; set; }
        public string NomeBloco { get; set; }
        public int CodLocal { get; set; }
        public string NomeLocal { get; set; }
        public int CodItem { get; set; }
        public string NomeItem { get; set; }
        public string Param { get; set; }
        public int CodStatus { get; set; }
        public string NomeStatus { get; set; }
        public string Conformidade { get; set; }
        public int Gravidade { get; set; }
        public string Medidas { get; set; }
        public string NomeImg { get; set; }

        public virtual ControleOs CodControleNavigation { get; set; }
    }
}
