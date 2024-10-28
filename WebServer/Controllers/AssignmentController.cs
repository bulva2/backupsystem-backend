using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebServer.Logic;
using WebServer.Models;

namespace WebServer.Controllers
{
    [Route("api/assignments")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public AssignmentController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public ActionResult<IEnumerable<Assignment>> GetAllAssignments()
        {
            return Ok(_context.Assignments.AsEnumerable());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin,user")]
        public ActionResult<Assignment> GetById([FromRoute] int id)
        {
            Assignment? assignment = _context.Assignments.Find(id);

            if (assignment == null)
                return NotFound();

            return Ok(assignment);
        }

        [HttpGet("uuid/{uuid}")]
        [Authorize(Roles = "admin, user")]
        public ActionResult<List<Assignment>> GetAssignmentsByUUID([FromRoute] Guid uuid)
        {
            List<Assignment> assignments = _context.Assignments.Where(a => uuid == a.ComputerUUID).ToList();

            if (assignments.Count == 0)
                return NotFound();

            return Ok(assignments);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult<Assignment> AssignAssignment([FromBody] Assignment assignment)
        {
            Console.WriteLine(assignment.ComputerUUID);
            Console.WriteLine(assignment.JobId);

            if (assignment == null || assignment.ComputerUUID == Guid.Empty || assignment.JobId <= 0)
            {
                return BadRequest("Invalid assignment data.");
            }

            assignment.Computer = _context.Computers.FirstOrDefault(c => c.UUID == assignment.ComputerUUID);
            assignment.Job = _context.Jobs.FirstOrDefault(j => j.Id == assignment.JobId);

            if (assignment.Computer == null || assignment.Job == null)
            {
                return BadRequest("Invalid computer or job reference.");
            }

            _context.Assignments.Add(assignment);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = assignment.Id }, assignment);
        }


        [HttpDelete("id/{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult Delete([FromRoute] int id)
        {
            Assignment? assignment = _context.Assignments.Find(id);

            if (assignment == null)
                return NotFound();

            _context.Assignments.Remove(assignment);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
