using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Cors;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Atarefado.Models;
using Atarefado.Controllers;
using Atarefado.Data;
using Atarefado.ViewModels;
using Atarefado.Services;
using Microsoft.EntityFrameworkCore;
namespace Atarefado.Controllers
{

    [ApiController]
    [Route("v1")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> AuthenticateAsync(
            [FromServices] AppDbContext context,
            [FromBody] Usuario model)
        {
            var usuario = await context.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.usuario == model.usuario && x.senha == model.senha);

            if (usuario == null)
                return NotFound(new { message = "Usuario ou senha invalidos" });

            var token = TokenService.GeneratedToken(usuario);

            usuario.senha = "";

            return new
            {
                usuario = usuario,
                token = token
            };
        }
    }
}