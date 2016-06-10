using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ILoveLampMusic.Models
{
    public class Album
    {
        public int AlbumId { get; set; }
        public string AlbumTitle { get; set; }
        public string YearReleased { get; set; }
        public string Artist { get; set; }
        public List<Track> Tracks { get; set; }
    }
}
