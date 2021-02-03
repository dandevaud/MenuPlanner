// <copyright file="MenuIngredient.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace MenuPlanner.Shared.models
{
    public class MenuIngredient
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required,ForeignKey("Id")]
        public Ingredient Ingredient { get; set; }
        [NotMapped]
        public Quantity Quantity { get; set; }
        [ForeignKey("Id")]
        public Menu Menu { get; set; }
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
