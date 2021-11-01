using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Atarefado.Models;
namespace Atarefado.Models
{
    public class Usuario
    {

        [Key()]
        public int id { get; set; }
        public string nome { get; set; }
        public string usuario { get; set; }
        public string senha { get; set; }
        public virtual List<Tarefa> tarefas { get; set; }

        

    }
}