﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lista10.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Lista10.Data
{
    public class ShopDbContext: IdentityDbContext
	{
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {

        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); //tables for Identity
            modelBuilder.Seed();
        }
       
    }
}
