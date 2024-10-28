using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebServer.Logic;
using WebServer.Models;
using WebServer.Validators;

namespace WebServer.Controllers
{
    [Route("api/jobs")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public JobController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public ActionResult<List<Job>> GetAllJobs()
        {
            return Ok(_context.Jobs.ToList());
        }

        [HttpGet("uuid/{uuid}")]
        [Authorize(Roles = "admin,user")]
        public ActionResult<List<Job>> GetJobsByUUID(Guid uuid)
        {
            List<Assignment> assignments = _context.Assignments
                .Where(u => u.ComputerUUID == uuid)
                .ToList();

            List<Job> jobs = new List<Job>();

            foreach (Assignment assignment in assignments) {
                if (assignment.Job != null)
                    jobs.Add(assignment.Job);
            }

            return Ok(jobs);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin,user")]
        public ActionResult<Job> GetById([FromRoute] int id)
        {
            Job? job = _context.Jobs.Find(id);

            if (job == null)
                return NotFound();

            return Ok(job);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult<Job> Create([FromBody] Job job)
        {
            JobValidator validator = new JobValidator();

            if (!validator.Validate(job))
                return BadRequest("Validation unsuccessful.");

            _context.Jobs.Add(job);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = job.Id }, job);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult Update([FromRoute] int id, [FromBody] Job job)
        {
            JobValidator validator = new JobValidator();

            if (!validator.Validate(job))
                return BadRequest("Validation unsuccessful.");

            if (job.Id != id)
            {
                return BadRequest();
            }

            // Smažeme starý sources/targets, určitě to jde líp, ale takovej cheat no.
            _context.Sources.Where(i => i.JobId == job.Id).ToList().ForEach(i => _context.Sources.Remove(i));
            _context.Targets.Where(i => i.JobId == job.Id).ToList().ForEach(i => _context.Targets.Remove(i));

            _context.Jobs.Update(job);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult Delete([FromRoute] int id)
        {
            Job? job = _context.Jobs.Find(id);

            if (job == null)
                return NotFound();

            _context.Jobs.Remove(job);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
