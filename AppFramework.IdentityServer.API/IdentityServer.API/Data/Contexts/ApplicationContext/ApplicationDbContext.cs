using System;
using IdentityServer.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.API.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ApplicationJobTitleTranslation>().HasKey(e => e.Id);
            //modelBuilder.Entity<ApplicationJobTitleTranslation>().HasIndex(e => e.Id).IsUnique();
            //modelBuilder.Entity<ApplicationJobTitleTranslation>()
            //    .HasOne(p => p.JobTitle)
            //    .WithMany(b => b.JobTitleTranslations);

            base.OnModelCreating(modelBuilder);
        }
        //public DbSet<ApplicationJobTitleTranslation> JobtitleTranslation { get; set; }

        //public DbSet<ApplicationJobTitle> Jobtitles { get; set; }

    }
}
