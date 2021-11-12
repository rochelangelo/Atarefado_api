using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using Atarefado.Data;
using Atarefado.Models;
using Atarefado.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
namespace Atarefado.Controllers
{

    [ApiController]
    [Route("v1")]
    public class TarefaController : ControllerBase
    {

        //somente para verificação do cascate no DB
        [HttpGet]
        [Route("tarefasTodos")]
        public async Task<IActionResult> GetAsyncTodos([FromServices] AppDbContext context)
        {
            var tarefas = await context.Tarefas
                .AsNoTracking()
                .ToArrayAsync();
            return Ok(tarefas);
        }

        [HttpGet]
        [Route("tarefasTodos/{id}")]
        public async Task<IActionResult> GetIdAsyncTodos([FromServices] AppDbContext context, [FromRoute] int id)
        {
            var tarefa = await context
            .Tarefas
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.id == id);
            return tarefa == null ? NotFound() : Ok(tarefa);
        }

        [HttpPost]
        [Route("tarefasTodos")]
        public async Task<IActionResult> PostAsyncTodos(
            [FromServices] AppDbContext context,
            [FromBody] CreateTarefaViewModel model)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            var tarefa = new Tarefa
            {
                data = model.Date,
                nome = model.Name,
                descricao = model.Description,
                flag = false,
                usuarioId = 0,
            };

            try
            {
                await context.Tarefas.AddAsync(tarefa);
                await context.SaveChangesAsync();
                return Created("v1/tarefasTodos/{tarefa.id}", tarefa);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

        [HttpPut]
        [Route("tarefaTodosFinalizada/{id}")]
        public async Task<IActionResult> PutFinishAsyncTodos(
            [FromServices] AppDbContext context,
            [FromRoute] int id)
        {

            var tarefa = await context.Tarefas.FirstOrDefaultAsync(x => x.id == id);

            if (tarefa == null)
                return NotFound("nao foi");

            try
            {

                tarefa.flag = true;


                context.Tarefas.Update(tarefa);
                await context.SaveChangesAsync();
                return Ok(tarefa);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

        /////////////


        [HttpGet]
        [Route("tarefas")]
        [Authorize]
        public async Task<IActionResult> GetAsync([FromServices] AppDbContext context)
        {
            var userid = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var tarefas = await context.Tarefas
                .AsNoTracking()
                .Where(x => x.usuarioId == userid)
                .ToArrayAsync();
            return tarefas == null ? NotFound(new {message = "Este usuario não possui tarefas"}) : Ok(tarefas);
        }

        [HttpGet]
        [Route("tarefas/{id}")]
        [Authorize]
        public async Task<IActionResult> GetIdAsync([FromServices] AppDbContext context, [FromRoute] int id)
        {
            var userid = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var tarefa = await context
            .Tarefas
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.usuarioId == userid && x.id == id);
            return tarefa == null ? NotFound() : Ok(tarefa);
        }

        [HttpPost]
        [Route("tarefas")]
        [Authorize]
        public async Task<IActionResult> PostAsync(
            [FromServices] AppDbContext context,
            [FromBody] CreateTarefaViewModel model)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            var userid = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var tarefa = new Tarefa
            {
                data = model.Date,
                nome = model.Name,
                descricao = model.Description,
                flag = false,
                usuarioId = userid,
            };

            try
            {
                await context.Tarefas.AddAsync(tarefa);
                await context.SaveChangesAsync();
                return Created("v1/tarefas/{tarefa.id}", tarefa);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

        [HttpPut]
        [Route("tarefas/{id}")]
        [Authorize]
        public async Task<IActionResult> PutAsync(
            [FromServices] AppDbContext context,
            [FromBody] CreateTarefaViewModel model,
            [FromRoute] int id)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            var userid = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var tarefa = await context.Tarefas.FirstOrDefaultAsync(x => x.usuarioId == userid && x.id == id);

            if (tarefa == null)
                return NotFound();

            try
            {

                tarefa.nome = model.Name;
                tarefa.data = model.Date;
                tarefa.descricao = model.Description;


                context.Tarefas.Update(tarefa);
                await context.SaveChangesAsync();
                return Ok(tarefa);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

        [HttpPut]
        [Route("tarefaFinalizada/{id}")]
        [Authorize]
        public async Task<IActionResult> PutFinishAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int id)
        {


            var userid = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var tarefa = await context.Tarefas.FirstOrDefaultAsync(x => x.id == id);

            if (tarefa == null)
                return NotFound("nao foi");

            try
            {

                tarefa.flag = true;


                context.Tarefas.Update(tarefa);
                await context.SaveChangesAsync();
                return Ok(tarefa);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

        [HttpDelete]
        [Route("tarefas/{id}")]

        public async Task<IActionResult> DeleteAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int id)
        {
            var userid = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var tarefa = await context.Tarefas.FirstOrDefaultAsync(x => x.usuarioId == userid && x.id == id);

            try
            {
                context.Tarefas.Remove(tarefa);
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