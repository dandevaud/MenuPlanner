// <copyright file="Menu.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MenuPlanner.Shared.models.enums;
using MenuPlanner.Shared.Models;
using MenuPlanner.Shared.Models.enums;

namespace MenuPlanner.Shared.models
{
    public class Menu : Entity
    {
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public TimeOfDay TimeOfDay { get; set; } //Flags

        public Diet Diet { get; set; }
        public Season Season { get; set; } //Flags
        [Range(1,int.MaxValue, ErrorMessage= "Please enter a value bigger than {1}")]
        public int Portion { get; set; } = 4;
        [StringLength(35, ErrorMessage = "Portion Description is to long (25 Character limit)")]
        public string PortionDescription { get; set; } = "Persons";
        [Required]
        public string Description { get; set; }
        [Required]
        public ICollection<string> Steps { get; set; }
        [Required, ForeignKey("MenuIngredientId")]
        public ICollection<MenuIngredient> Ingredients { get; set; } = new List<MenuIngredient>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public double AverageRating { get; set; }
        public int Votes { get; set; }
        public MenuCategory MenuCategory { get; set; } //Flags
        public ICollection<Image> Images { get; set; } = new List<Image>();
        public Uri Video { get; set; }

        public int PrepTime { get; set; }
        public int CookTime { get; set; }


    }
}
