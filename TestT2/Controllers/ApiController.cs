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
                return BadRequest("�������� ������");
            }

            if (entities.Any(x => x.Id == entity.Id))
            {
                return Conflict("�������� � ����� ��������������� ��� ����������");
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
                return NotFound($"�������� � ��������������� {get} �� �������");
            }

            return Ok(e);
        }
    }
}