// <copyright file="Comment.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MenuPlanner.Shared.Models;

namespace MenuPlanner.Shared.models
{
    public class Comment : Identifier
    {
        [Required]
        public Menu Menu { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public User User { get; set; }
    }
}
