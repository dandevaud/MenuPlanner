// <copyright file="Menu.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;

namespace MenuPlanner.Shared.models
{
    public class Menu
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public int TimeOfDay { get; set; } //Flags
        public int Season { get; set; } //Flags
        public string Description { get; set; }
        public List<string> Steps { get; set; }
        public Dictionary<Ingredient, Quantity> Ingredients { get; set; }
        public List<Comment> Comments { get; set; }
        public double AverageRating { get; set; }
        public int Votes { get; set; }

        public int MenuCategory { get; set; } //Flags
        // Ev on Ingredient
        public int Calories { get; set; }
        public double Price { get; set; }
        // End
        public List<Image> Images { get; set; }
        public Uri Video { get; set; }
    }
}
