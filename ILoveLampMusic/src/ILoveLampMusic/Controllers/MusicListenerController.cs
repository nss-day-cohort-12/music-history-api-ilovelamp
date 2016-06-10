using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using ILoveLampMusic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

//For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860
//Postman Route:  http://localhost:5000/api/MusicListener

namespace ILoveLampMusic.Controllers
{
    [Produces("application/json")]
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/MusicListener")]
    public class MusicListenerController : Controller
    {
        private MusicHistoryContext _context;

        public MusicListenerController(MusicHistoryContext context)
        {
            _context = context;
        }

        // GET: api/MusicListeners
        [HttpGet]
        public IActionResult Get([FromQuery] string username)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IQueryable<MusicListener> Listeners = from aGuy in _context.MusicListener
                                                  select new MusicListener
                                                  {
                                                      MusicListenerId = aGuy.MusicListenerId,
                                                      Name = aGuy.Name,
                                                      Email = aGuy.Email,
                                                  };

            if (username != null)
            {
                Listeners = Listeners.Where(g => g.Name == username);
            }

            if (Listeners == null)
            {
                return NotFound();
            }

            return Ok(Listeners);
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetListener")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MusicListener listener = _context.MusicListener.Single(l => l.MusicListenerId == id);

            if (listener == null)
            {
                return NotFound();
            }

            return Ok(listener);
        }


        //POST api/values
       [HttpPost]
        public IActionResult Post([FromBody]MusicListener listener)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var existingUser = from g in _context.MusicListener
                               where g.Name == listener.Name
                               select g;

            if (existingUser.Count<MusicListener>() > 0)
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }


            _context.MusicListener.Add(listener);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MusicListenerExists(listener.MusicListenerId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetListener", new { id = listener.MusicListenerId }, listener);
        }

        //PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] MusicListener listener)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != listener.MusicListenerId)
            {
                return BadRequest();
            }

            _context.Entry(listener).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MusicListenerExists(listener.MusicListenerId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }

        //DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MusicListener listener = _context.MusicListener.Single(m => m.MusicListenerId == id);
            if (listener == null)
            {
                return NotFound();
            }

            _context.MusicListener.Remove(listener);
            _context.SaveChanges();

            return Ok(listener);
        }

        private bool MusicListenerExists(int id)
        {
            return _context.MusicListener.Count(e => e.MusicListenerId == id) > 0;
        }
    }
}
