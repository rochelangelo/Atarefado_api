using System.ComponentModel.DataAnnotations;
using System;
namespace Atarefado.ViewModels
{
    public class CreateTarefaViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}