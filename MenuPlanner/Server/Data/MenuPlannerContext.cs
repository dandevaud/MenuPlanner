// <copyright file="MenuPlannerContext.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Linq;
using MenuPlanner.Shared.models;
using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;


namespace MenuPlanner.Server.Data
{
    public class MenuPlannerContext : DbContext
    {
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<MenuIngredient> MenuIngredients { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
       
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Image> Images { get; set; }
       

        public MenuPlannerContext(DbContextOptions<MenuPlannerContext> dbContextOptions) : base(dbContextOptions)
        {
            Database.Migrate();
        }

        public MenuPlannerContext() : base()
        {
           
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredient>(entity => entity.Property(m => m.Id).HasMaxLength(255));
            modelBuilder.Entity<MenuIngredient>(entity => entity.Property(m => m.Id).HasMaxLength(255));
            modelBuilder.Entity<Menu>(entity => entity.Property(m => m.Id).HasMaxLength(255));
            modelBuilder.Entity<Tag>(entity => entity.Property(m => m.Id).HasMaxLength(255));
            modelBuilder.Entity<Comment>(entity => entity.Property(m => m.Id).HasMaxLength(255));
            modelBuilder.Entity<Image>(entity => entity.Property(m => m.Id).HasMaxLength(255));

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
