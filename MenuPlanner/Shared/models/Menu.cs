// <copyright file="Menu.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MenuPlanner.Shared.models
{
    public class Menu
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid MenuId { get; set; }
        [Required]
        public string Name { get; set; }
        public int TimeOfDay { get; set; } //Flags
        public int Season { get; set; } //Flags
        [Required]
        public string Description { get; set; }
        //[Required]
        public ICollection<string> Steps { get; set; }
        //[Required]
        public ICollection<MenuIngredient> Ingredients { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public double AverageRating { get; set; }
        public int Votes { get; set; }

        public int MenuCategory { get; set; } //Flags
        public ICollection<Image> Images { get; set; }
        public Uri Video { get; set; }

    }

}
