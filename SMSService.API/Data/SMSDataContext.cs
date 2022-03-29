using Microsoft.EntityFrameworkCore;
using SMSSerivce.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSService.API.Data
{
    public class SMSDataContext : DbContext
    {
        public SMSDataContext(DbContextOptions<SMSDataContext> options) : base(options)
        {
        }
        public DbSet<Account> Accounts { get; set; }

        public DbSet<PhoneNumber> PhoneNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                   .HasMany(a => a.PhoneNumbers)
                   .WithOne(p => p.Account);


        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }

    }
}
