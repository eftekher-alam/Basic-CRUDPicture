using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicCRUDPicture.Models
{
    public class PictureDbContext: DbContext
    {
        public PictureDbContext(DbContextOptions<PictureDbContext> options):base(options)
        {

        }
        public DbSet<Picture> Pictures { get; set; }
    }
}
