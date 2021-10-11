using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoWebApi.Controllers
{
    [Route("api/todo-items")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private static readonly List<string> items = new List<string>
        {
            "Hausübung machen",
            "Für PLF lernen",
            "Katze füttern"
        };

        [HttpGet]
        public IActionResult GetAllItems()
        {
            return Ok(items);
        }

        [HttpGet]
        [Route("{index}", Name = "GetSpecificItem")]
        public IActionResult GetItem(int index)
        {
            if (index < 0 || index >= items.Count)
            {
                return BadRequest("Invalid index");
            }

            return Ok(items[index]);
        }

        [HttpPost]
        public IActionResult AddItem([FromBody] string newItem)
        {
            items.Add(newItem);
            return CreatedAtRoute("GetSpecificItem",
                new { index = items.IndexOf(newItem) },
                newItem);
        }

        [HttpPut]
        [Route("{index}")]
        public IActionResult UpdateItem(int index, [FromBody] string newItem)
        {
            if (index < 0 || index >= items.Count)
            {
                return BadRequest("Invalid index");
            }

            items[index] = newItem;
            return Ok();
        }

        [HttpDelete]
        [Route("{index}")]
        public IActionResult DeleteItem(int index)
        {
            if (index < 0 || index >= items.Count)
            {
                return BadRequest("Invalid index");
            }

            items.RemoveAt(index);
            return NoContent();
        }
    }
}
