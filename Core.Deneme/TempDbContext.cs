using Core.Deneme.Features;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Deneme
{
    public class TempDbContext : DbContext
    {
        public DbSet<Isyeri> Isyeri { get; set; }
    }
}
