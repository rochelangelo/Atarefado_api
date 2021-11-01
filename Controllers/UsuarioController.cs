using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Atarefado.Data;
using Atarefado.Models;
using Atarefado.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Atarefado.Controllers
{

    [ApiController]
    [Route("v1")]
    public class UsuarioController : ControllerBase
    {
        [HttpGet]
        [Route("usuario")]
        public async Task<IActionResult> GetAsync([FromServices] AppDbContext context)
        {
            var usarios = await context.Usuarios.AsNoTracking().ToListAsync();
            return Ok(usarios);
        }

        [HttpGet]
        [Route("usuario/{id}")]
        public async Task<IActionResult> GetIdAsync([FromServices] AppDbContext context, [FromRoute] int id)
        {
            var usuario = await context
            .Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.id == id);
            return usuario == null ? NotFound() : Ok(usuario);
        }

        [HttpPost]
        [Route("usuario")]
        public async Task<IActionResult> PostAsync(
            [FromServices] AppDbContext context,
            [FromBody] CreateUsuarioViewModel model)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            var usuario = new Usuario
            {
                nome = model.Name,
                usuario = model.User,
                senha = model.Password,
            };

            try
            {
                await context.Usuarios.AddAsync(usuario);
                await context.SaveChangesAsync();
                return Created("v1/usuario/{usuario.id}", usuario);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

        [HttpPut]
        [Route("usuario/{id}")]
        public async Task<IActionResult> PutAsync(
            [FromServices] AppDbContext context,
            [FromBody] CreateUsuarioViewModel model,
            [FromRoute] int id)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            var usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.id == id);

            if (usuario == null)
                return NotFound();

            try
            {

                usuario.nome = model.Name;
                usuario.usuario = model.User;
                usuario.senha = model.Password;


                context.Usuarios.Update(usuario);
                await context.SaveChangesAsync();
                return Ok(usuario);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

        [HttpDelete]
        [Route("usuario/{id}")]

        public async Task<IActionResult> DeleteAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int id)
        {
            var usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.id == id);

            try
            {
                context.Usuarios.Remove(usuario);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

    }
}