using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ILoveLampMusic.Models
{
    public class MusicHistoryContext: DbContext 
    {
        //public  MusicHistoryContext ()
        //{   
            
        //}
        public MusicHistoryContext(DbContextOptions<MusicHistoryContext> options)
           : base(options)
        { }

       public DbSet<Album> Album { get; set; }
       public DbSet<MusicListener> MusicListener { get; set; }
       public DbSet<Track> Track { get; set; }
        
    }
}

