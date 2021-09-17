using System;
using System.Collections.Generic;
using SantafeApi.Infraestrucutre.Data;

#nullable disable

namespace SantafeApi.Entities
{
    public partial class Cliente
    {
        public Cliente()
        {
            Blocos = new HashSet<Bloco>();
            ControleOs = new HashSet<ControleOs>();
        }

        public int CodCliente { get; set; }
        public string NomeCliente { get; set; }
        public string CnpjCliente { get; set; }
        public string TecResponsavel { get; set; }
        public string EnderecoCliente { get; set; }
        public string TipoDoLocal { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string CodUsuario { get; set; }
        public string DataCad { get; set; }

        public virtual SantafeApiUser SantafeApiUser { get; set; }
        public virtual ICollection<Bloco> Blocos { get; set; }
        public virtual ICollection<ControleOs> ControleOs { get; set; }
    }
}
