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
    }
}