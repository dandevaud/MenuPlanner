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
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<IngredientToIngredient> IngredientToIngredients { get; set; }

        public MenuPlannerContext(DbContextOptions<MenuPlannerContext> dbContextOptions) : base(dbContextOptions)
        {
            Database.EnsureCreated();
        }

        public MenuPlannerContext() : base(){}


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
                .Entity<IngredientToIngredient>()
                .HasKey(ii => new {ii.Parent, ii.Child});
            modelBuilder
                .Entity<Ingredient>()
                .HasIndex(i => i.Name)
                .IsUnique();
            modelBuilder
                .Entity<IngredientToIngredient>()
                .HasOne<Ingredient>(ing => ing.Parent)
                .WithMany(ing => ing.ChildIngredients)
                .HasForeignKey(sc => sc.ParentId);

            modelBuilder
                .Entity<IngredientToIngredient>()
                .HasOne<Ingredient>(ing => ing.Child)
                .WithMany(ing => ing.ParentIngredients)
                .HasForeignKey(sc => sc.ChildId);

            modelBuilder
                .Entity<Ingredient>()
                .HasMany<Ingredient>(ing => ing.SimilarIngredients)
                .WithMany(ing => ing.SimilarIngredients);
                

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
