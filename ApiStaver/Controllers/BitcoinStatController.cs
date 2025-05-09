using ApiStaver.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiStaver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BitcoinStatController : ControllerBase
    {
        private readonly StatServerContext _context;

        public BitcoinStatController(StatServerContext context)
        {
            _context = context;
        }

        //Get : api/BitcoinStat
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BitcoinStat>>> GetBitcoinStats()
        {
            if (_context.BitcoinStats is null)
            {
                return NotFound();
            }
            return await _context.BitcoinStats.ToListAsync();
        }
    }
}
