﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using BookingClone.Pages;

namespace BookingClone.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);      
            builder.Entity<BookModel>()
                .HasMany(c => c.ImagesUploaded)
                .WithOne(e => e.BookModel);
        }

        public DbSet<BookModel> BookModel { get; set; }

        public DbSet<GuestDetails> GuestDetails { get; set; }

        public DbSet<Image> Image { get; set; }



    }
}
