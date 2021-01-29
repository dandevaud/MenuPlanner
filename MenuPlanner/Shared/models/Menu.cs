// <copyright file="Menu.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MenuPlanner.Shared.models.enums;

namespace MenuPlanner.Shared.models
{
    public class Menu
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid MenuId { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public TimeOfDay TimeOfDay { get; set; } //Flags
        public Season Season { get; set; } //Flags
        public int Portionen { get; set; }
        public string PortionenDescription { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public ICollection<string> Steps { get; set; }
        [Required]
        public ICollection<MenuIngredient> Ingredients { get; set; } = new List<MenuIngredient>();
        public ICollection<Comment> Comments { get; set; }
        public double AverageRating { get; set; }
        public int Votes { get; set; }
        public MenuCategory MenuCategory { get; set; } //Flags
        public ICollection<Image> Images { get; set; }
        public Uri Video { get; set; }
    }
}
