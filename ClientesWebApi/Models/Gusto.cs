using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClientesWebApi.Models
{
    [Table("Gustos")]
    public class Gusto
    {
        public int ID { get; set; }

        public string Nombre { get; set; }
      
        public List<Cliente> Clientes { get; set; }
    }
}