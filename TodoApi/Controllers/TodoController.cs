using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using System.Linq;

namespace TodoApi.Controllers
{
    [Route("api/v1/homes/")]
    public class TodoController : Controller
    {
        private readonly SensorDataContext _context;

        public TodoController(SensorDataContext context)
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

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(long id)
        {
            var item = _context.SensorData.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] SensorData item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.SensorData.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] SensorData item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var todo = _context.SensorData.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.Name = item.Name;
            todo.Temperature = item.Temperature;
            todo.Humidity = item.Humidity;
            todo.Soil = item.Soil;
            todo.Motion = item.Motion;

        _context.SensorData.Update(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.SensorData.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.SensorData.Remove(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }

    }
}