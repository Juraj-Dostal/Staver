using ApiStaver.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiStaver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TempHumSensorController : ControllerBase
    {

        private readonly StatServerContext _context;

        public TempHumSensorController(StatServerContext context)
        {
            _context = context;
        }

        // GET: api/TempHumSensor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TempHumSensor>>> GetTempHumSensor()
        {
            if (_context.TempHumSensors is null)
            {
                return NotFound();
            }

            return await _context.TempHumSensors.ToListAsync();
        }

        // GET api/TempHumSensorController/loction/<location>
        [HttpGet("location/{room}")]
        public async Task<ActionResult<IEnumerable<TempHumSensor>>> GetTempHumSensor(string room)
        {
            if (_context.TempHumSensors is null)
            {
                return NotFound();
            }

            return await _context.TempHumSensors.Where(x => x.Location.Equals(room)).ToListAsync();
        }

        // GET api/<TempHumSensorController>/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TempHumSensor>> GetTempHumSensor(int id)
        {
            if (_context.TempHumSensors is null)
            {
                return NotFound();
            }

            var tempHumSensor = await _context.TempHumSensors.FindAsync(id);

            if (tempHumSensor == null)
            {
                return NotFound();
            }

            return tempHumSensor;
        }

        // POST api/<TempHumSensorController>
        [HttpPost]
        public async Task<ActionResult<TempHumSensor>> PostTempHumSensor(TempHumSensor data)
        {
            _context.TempHumSensors.Add(data);
            await _context.SaveChangesAsync();

            return Ok(data);
        }

        // PUT api/<TempHumSensorController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<TempHumSensor>> PutTempHumSensor(int id, [FromBody] TempHumSensor pData)
        {
            if (id != pData.Id)
            {
                return BadRequest("ID mismatch");
            }

            if (_context.TempHumSensors is null)
            {
                return NotFound();
            }

            var data = await _context.TempHumSensors.FindAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            data.Temperature = pData.Temperature;
            data.Humidity = pData.Humidity;
            data.Location = pData.Location;

            _context.Entry(data).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(data);


        }

        // DELETE api/<TempHumSensorController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TempHumSensor>> DeleteTempHumSensor(int id)
        {

            if (_context.TempHumSensors is null)
            {
                return NotFound();
            }

            var tempHumSensor = await _context.TempHumSensors.FindAsync(id);

            if (tempHumSensor == null)
            {
                return NotFound();
            }
            
            _context.TempHumSensors.Remove(tempHumSensor);
            _context.SaveChanges();

            return Ok(tempHumSensor);

        }
    }
}
