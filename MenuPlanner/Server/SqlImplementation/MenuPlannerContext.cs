// <copyright file="MenuPlannerContext.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Linq;
using MenuPlanner.Shared.models;
using Microsoft.EntityFrameworkCore;


namespace MenuPlanner.Server.SqlImplementation
{
    public class MenuPlannerContext : DbContext
    {
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }

        public MenuPlannerContext(DbContextOptions<MenuPlannerContext> dbContextOptions) : base(dbContextOptions)
        {
            Database.EnsureCreated();


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //https://docs.microsoft.com/en-us/ef/core/modeling/
            //https://stackoverflow.com/questions/20711986/entity-framework-code-first-cant-store-liststring
            modelBuilder.Entity<Menu>()
                .Property(e => e.Steps)
                .HasConversion(
                    v => string.Join("|", v),
                    v => v.Split('|', StringSplitOptions.RemoveEmptyEntries).ToList()
             );
            modelBuilder
                .Entity<Ingredient>()
                .HasIndex(i => i.Name)
                .IsUnique();

            modelBuilder
                .Entity<Menu>()
                .HasIndex(i => i.Name)
                .IsUnique();

        }
    }
}
