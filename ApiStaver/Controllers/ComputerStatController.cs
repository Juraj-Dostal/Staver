using ApiStaver.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiStaver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputerStatController : ControllerBase
    {
        private readonly StatServerContext _context;

        public ComputerStatController(StatServerContext context)
        {
            _context = context;
        }

        //Get : api/ComputerStat
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ComputerStat>>> GetComputerStats()
        {
            if (_context.ComputerStats is null)
            {
                return NotFound();
            }

            return await _context.ComputerStats.ToListAsync();
        }
    }
}
