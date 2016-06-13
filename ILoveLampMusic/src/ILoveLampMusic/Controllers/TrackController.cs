using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using ILoveLampMusic.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ILoveLampMusic.Controllers
{
    [Produces("application/json")]
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/Track")]
    public class TrackController : Controller
    {
        private MusicHistoryContext _context;
        public TrackController(MusicHistoryContext context)
        {
            _context = context;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult Get([FromQuery] string username)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IQueryable<Track> Songs = from aBallad in _context.Track
                                                  select new Track
                                                  {
                                                      TrackId = aBallad.TrackId,
                                                      Author = aBallad.Author,
                                                      Genre = aBallad.Genre,
                                                      Length = aBallad.Length,
                                                      Musiclistener=aBallad.Musiclistener ,
                                                      Title =aBallad.Title ,
                                                      AlbumId = aBallad.AlbumId,

                                                  };


            if (Songs == null)
            {
                return NotFound();
            }

            return Ok(Songs);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
