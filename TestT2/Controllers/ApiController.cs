using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestT2.Models;

namespace TestT2.Controllers
{
    [ApiController]
    [Route("/")]
    public class ApiController : ControllerBase
    {
        private static List<Entity> entities = new();

        [HttpPost("insert")]
        public IActionResult AddAsync(string insert)
        {
            var entity = JsonConvert.DeserializeObject<Entity>(insert);

            if (entity == null || entity.Id == Guid.Empty)
            {
                return BadRequest("Неверный запрос");
            }

            if (entities.Any(x => x.Id == entity.Id))
            {
                return Conflict("Сущность с таким идентификатором уже существует");
            }

            entities.Add(entity);

            return Ok(entity);
        }

        [HttpGet("get")]
        public IActionResult GetAsync(Guid get)
        {
            var e = entities.FirstOrDefault(x => x.Id == get);
            if(e is null)
            {
                return NotFound($"Сущность с идентификатором {get} не найдена");
            }

            return Ok(e);
        }
    }
}