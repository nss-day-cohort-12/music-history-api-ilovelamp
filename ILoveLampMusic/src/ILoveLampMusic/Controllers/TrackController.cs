using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using ILoveLampMusic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ILoveLampMusic.Controllers
{
    [Produces("application/json")]
    [EnableCors("AllowNewDevelopmentEnvironment")]
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
                                          Musiclistener = aBallad.Musiclistener,
                                          Title = aBallad.Title,
                                          AlbumId = aBallad.AlbumId,

                                      };


            if (Songs == null)
            {
                return NotFound();
            }

            return Ok(Songs);
        }

        // GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Track tracks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var existingSong = from t in _context.Track
                               select new Track

                               {
                                   Author = tracks.Author,
                                   Genre = tracks.Genre,
                                   Length = tracks.Length,
                                   Musiclistener = tracks.Musiclistener,
                                   Title = tracks.Title,
                                   AlbumId = tracks.AlbumId,
                               };

            if (existingSong.Count<Track>() > 0)
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }


            _context.Track.Add(tracks);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ExistingTrack(tracks.TrackId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetListener", new { id = tracks.TrackId }, tracks);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Track tracks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tracks.TrackId)
            {
                return BadRequest();
            }

            _context.Entry(tracks).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExistingTrack(tracks.TrackId))
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

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Track tracks = _context.Track.Single(m => m.TrackId == id);
            if (tracks == null)
            {
                return NotFound();
            }

            _context.Track.Remove(tracks);
            _context.SaveChanges();

            return Ok(tracks);
        }

        private bool ExistingTrack(int id)
        {
            return _context.Track.Count(e => e.TrackId == id) > 0;
        }
    }
}