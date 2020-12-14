using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controllers
{
    [ApiController]
    [Route("v1/comments")]
    public class CommentsController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Comment>>> Get([FromServices] DataContext context)
        {
            var comment = await context.Comments.AsNoTracking().ToListAsync();
            return Ok(comment);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<List<Comment>>> Post([FromBody] Comment model, [FromServices] DataContext context)
        {
            context.Comments.Add(model);
            await context.SaveChangesAsync();
            
            return Ok(model);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<ActionResult<List<Comment>>> Put(Guid id, [FromBody] Comment model, [FromServices] DataContext context)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(id != model.Id)
                return NotFound("Comentário não encontrado");
            
            try
            {
                context.Entry(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch(Exception)
            {
                return BadRequest(new { Message = "Não foi possível criar o comentário" });
            }
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<ActionResult<List<Comment>>> Delete(Guid id, [FromServices] DataContext context)
        {
            var comment = await context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            
            if(comment == null)
                return NotFound("Comentário não encontrado");
            
            try
            {
                context.Comments.Remove(comment);
                await context.SaveChangesAsync();
                return Ok(new { Message = "Comentário deletado!" });
            }
            catch(Exception)
            {
                return BadRequest(new { Message = "Não foi apagar o comentário!" });
            }
        }
    }
}