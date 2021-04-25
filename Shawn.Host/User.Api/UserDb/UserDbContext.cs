
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using User.Api.Model;
using User.Api.UserDb.EntityFrameworkCoreConfig;

namespace User.Api.UserDb
{
    public class UserDbContext: DbContext
    {
        public virtual DbSet<UserInfo> UserInfo { get; set; }
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}
