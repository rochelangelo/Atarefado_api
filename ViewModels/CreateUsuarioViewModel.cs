using System.ComponentModel.DataAnnotations;
using System;
namespace Atarefado.ViewModels
{
    public class CreateUsuarioViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string User { get; set; }
        [Required]
        public string Password { get; set; }
    }
}