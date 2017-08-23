using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using System.Linq;

namespace TodoApi.Controllers
{
    [Route("api/v1/homes/")]
    public class SensorDataController : Controller
    {
        private readonly SensorDataContext _context;

        public SensorDataController(SensorDataContext context)
        {
            _context = context;

            if (_context.SensorData.Count() == 0)
            {
                _context.SensorData.Add(new SensorData { Name = "" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<SensorData> GetAll()
        {
            return _context.SensorData.ToList();
        }

        [HttpGet("hus{id}/data", Name = "GetSensorReadings")]
        public IActionResult GetById(long id)
        {
            var item = _context.SensorData.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost("hus{id}/data")]
        public IActionResult Create([FromBody] SensorData item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.SensorData.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetSensorReadings", new { id = item.Id }, item);
        }

        [HttpPut("hus{id}/data")]
//        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] SensorData item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var sensorReading = _context.SensorData.FirstOrDefault(t => t.Id == id);
            if (sensorReading == null)
            {
                return NotFound();
            }

            sensorReading.Name = item.Name;
            sensorReading.Temperature = item.Temperature;
            sensorReading.Humidity = item.Humidity;
            sensorReading.Soil = item.Soil;
            sensorReading.Motion = item.Motion;

        _context.SensorData.Update(sensorReading);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("hus{id}/data")]
        public IActionResult Delete(long id)
        {
            var sensorReading = _context.SensorData.FirstOrDefault(t => t.Id == id);
            if (sensorReading == null)
            {
                return NotFound();
            }

            _context.SensorData.Remove(sensorReading);
            _context.SaveChanges();
            return new NoContentResult();
        }

    }
}