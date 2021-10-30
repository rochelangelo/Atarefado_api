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
    public class AtarefadoController : ControllerBase
    {

        [HttpGet]
        [Route("tarefas")]
        public async Task<IActionResult> GetAsync([FromServices] AppDbContext context)
        {
            var tarefas = await context.Tarefas.AsNoTracking().ToListAsync();
            return Ok(tarefas);
        }

        [HttpGet]
        [Route("tarefas/{id}")]
        public async Task<IActionResult> GetIdAsync([FromServices] AppDbContext context, [FromRoute] int id)
        {
            var tarefa = await context
            .Tarefas
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.id == id);
            return tarefa == null ? NotFound() : Ok(tarefa);
        }

        [HttpPost]
        [Route("tarefas")]
        public async Task<IActionResult> PostAsync(
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
        public async Task<IActionResult> PutAsync(
            [FromServices] AppDbContext context,
            [FromBody] CreateTarefaViewModel model,
            [FromRoute] int id)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            var tarefa = await context.Tarefas.FirstOrDefaultAsync(x => x.id == id);

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

        [HttpDelete]
        [Route("tarefas/{id}")]

        public async Task<IActionResult> DeleteAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int id)
        {
            var tarefa = await context.Tarefas.FirstOrDefaultAsync(x => x.id == id);

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