// <copyright file="Ingredient.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MenuPlanner.Shared.models.enums;

namespace MenuPlanner.Shared.models
{
    public class Ingredient
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid IngredientId { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Ingredient> ParentIngredients { get; set; }
        public ICollection<Ingredient> ChildIngredients { get; set; }
        public IngredientCategory Category { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed.")]
        public int Calories { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Only positive number allowed.")]
        public double Price { get; set; }
    }
}
