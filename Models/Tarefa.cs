using System;
namespace Atarefado.Models
{
    public class Tarefa
    {
        public int  id { get; set; }
        public string nome { get; set; }
        public DateTime data { get; set; }
        public string descricao { get; set; }
        public bool flag { get; set; }
    }
}