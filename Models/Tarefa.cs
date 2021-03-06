using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Atarefado.Models
{
    public class Tarefa
    {
        [Key()]
        public int  id { get; set; }
        public string nome { get; set; }
        public DateTime data { get; set; }
        public string descricao { get; set; }
        public bool flag { get; set; }
        [ForeignKey("Usuario")]
        public int usuarioId { get; set; }
        public virtual Usuario usuario { get; set; }
    }
}