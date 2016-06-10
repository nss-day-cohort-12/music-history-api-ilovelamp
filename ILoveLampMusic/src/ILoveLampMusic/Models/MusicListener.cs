using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ILoveLampMusic.Models
{
    public class MusicListener
    {
        [Key]
        public int MusicListenerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<Track> Track { get; set; }
    }
}
