using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebServer.Logic;
using WebServer.Models;

namespace WebServer.Controllers
{
	[Route("api/reports")]
	[ApiController]
	public class ReportController : ControllerBase
	{
		private readonly DatabaseContext _context;

        public ReportController(DatabaseContext context)
        {
            _context = context;
        }

		[HttpGet]
		[Authorize(Roles = "admin")]
		public ActionResult<List<Report>> GetAll()
		{
			return Ok(_context.Reports.ToList());
		}

		[HttpGet("{uuid}")]
		[Authorize(Roles = "admin")]
		public ActionResult<Report> GetByUUID([FromBody] Guid uuid)
		{
			Report? report = _context.Reports.Find(uuid);

			if (report == null)
				return NotFound();

			return Ok(report);
		}

		[HttpPost]
		[Authorize(Roles = "admin")]
		public ActionResult<Report> Create([FromBody] Report report)
		{

            Console.WriteLine(report.UUID);

            _context.Reports.Add(report);
			_context.SaveChanges();

			return CreatedAtAction(nameof(GetByUUID), new {uuid = report.UUID}, report);
		}

		[HttpDelete]
		[Authorize(Roles = "admin")]
		public ActionResult Delete([FromRoute] int id)
		{
			Report? report = _context.Reports.Find(id);

			if (report == null)
				return NotFound();

			_context.Reports.Remove(report);
			_context.SaveChanges();
			return NoContent();
		}

	}
}
