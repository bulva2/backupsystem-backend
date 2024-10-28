using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebServer.Logic;
using WebServer.Models;

namespace WebServer.Controllers
{
    [Route("api/computers")]
    [ApiController]
    public class ComputerController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ComputerController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public ActionResult<IEnumerable<Computer>> GetAll()
        {
            IEnumerable<Computer> computers = _context.Computers.AsNoTracking().AsEnumerable();
            return Ok(computers);
        }

        [HttpGet("{uuid}")]
        [Authorize(Roles = "admin,user")]
        public ActionResult<Computer> GetById([FromRoute]Guid uuid)
        {
            Computer? computer = _context.Computers.Find(uuid);

            if (computer == null)
                return NotFound();

            return Ok(computer);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult<Computer> Create([FromBody]Computer computer)
        {
            _context.Computers.Add(computer);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { uuid = computer.UUID }, computer);
        }

        [HttpPut("{uuid}")]
        [Authorize(Roles = "admin")]
        public ActionResult Update([FromRoute]Guid uuid, [FromBody]Computer computer)
        {
            if (computer.UUID != uuid)
            {
                return BadRequest();
            }

            _context.Computers.Update(computer);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{uuid}")]
        [Authorize(Roles = "admin")]
        public ActionResult Delete([FromRoute]Guid uuid)
        {
            Computer? computer = _context.Computers.Find(uuid);

            if (computer == null)
                return NotFound();

            _context.Computers.Remove(computer);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
