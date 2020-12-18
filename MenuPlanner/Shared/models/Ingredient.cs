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
        public List<Ingredient> ParentIngredients { get; set; }
        public List<Ingredient> ChildIngredients { get; set; }
        public IngredientCategory Category { get; set; }
        public int Calories { get; set; }
        public double Price { get; set; }


    }
}
