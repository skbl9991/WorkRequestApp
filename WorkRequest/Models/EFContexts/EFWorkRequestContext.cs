using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkRequestManagment.Models.EFJunctions;

namespace WorkRequestManagment.Models.EFContexts
{
    public class EFWorkRequestContext : DbContext
    {
        public EFWorkRequestContext(DbContextOptions<EFWorkRequestContext> options) : base (options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<WorkRequest> WorkRequests { get; set; }
        public DbSet<WorkRequestUserJunction> WorkRequestUserJunctions { get; set; }
    }
}
