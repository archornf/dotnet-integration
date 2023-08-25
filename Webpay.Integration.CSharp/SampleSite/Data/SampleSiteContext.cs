using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleSite.Models;

namespace SampleSite.Data
{
    public class SampleSiteContext : DbContext
    {
        public SampleSiteContext (DbContextOptions<SampleSiteContext> options)
            : base(options)
        {
        }

        public DbSet<SampleSite.Models.Product> Product { get; set; } = default!;
    }
}
