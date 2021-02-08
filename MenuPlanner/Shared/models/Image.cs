// <copyright file="Image.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MenuPlanner.Shared.Models;

namespace MenuPlanner.Shared.models
{
    public class Image : Entity
    {
      
        public string AlternativeName { get; set; }
        public string Path { get; set; }
        [NotMapped]
        public byte[] ImageBytes { get; set; } = new byte[0];
    }
}
