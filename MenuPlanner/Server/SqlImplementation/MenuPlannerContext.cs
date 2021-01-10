// <copyright file="MenuPlannerContext.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Linq;
using MenuPlanner.Shared.models;
using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;


namespace MenuPlanner.Server.SqlImplementation
{
    public class MenuPlannerContext : DbContext
    {
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<MenuIngredient> MenuIngredients { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<User> Users { get; set; }
        //why table was created as "Image" and not as "Images"?
        public virtual DbSet<Image> Image { get; set; }

        public MenuPlannerContext(DbContextOptions<MenuPlannerContext> dbContextOptions) : base(dbContextOptions)
        {
            //Database.EnsureCreated();
            Database.Migrate();
        }

        public MenuPlannerContext() : base()
        {
            
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
            modelBuilder
                .Entity<Quantity>()
                .HasNoKey();


        }
    }
}
