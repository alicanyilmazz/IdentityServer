using IdentityServer.API2.Core.Dtos.StoredProcedureDtos;
using IdentityServer.API2.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.API2.Data.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<ImageFile> ImageFile { get; set; }
        public DbSet<ImageFileDetail> ImageFileDetail { get; set; }
        public DbSet<ImageQuality> ImageQuality { get; set; }

        //Comment For Migration
        //public DbSet<ImageQualityResponse> ImageQualityResult { get; set; }
        //public DbSet<ServerImagesInformation> ServerImagesInformation { get; set; }
        //public DbSet<ImageFileInformation> ImageFileInformation { get; set; }
        //public DbSet<ImageIndex> ImageIndex { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
