// <copyright file="MenuIngredient.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using MenuPlanner.Shared.Models;

namespace MenuPlanner.Shared.models
{
    public class MenuIngredient : Identifier
    {

        [Required]
        public Ingredient Ingredient { get; set; }
        [NotMapped]
        public Quantity Quantity { get; set; }
        public Menu Menu { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [MaxLength(32, ErrorMessage = "Please define a value with {1} characters only")]
        public string Grouping { get; set; }

        [Required]
        //https://stackoverflow.com/questions/8973027/ef-code-first-map-dictionary-or-custom-type-as-an-nvarchar
        public string QuantityAsJson
        {
            get
            {
                return JsonSerializer.Serialize(Quantity);
            }
            set
            {
                Quantity = value != null ? JsonSerializer.Deserialize<Quantity>(value) : null;
            }
        }
        
    }
}
