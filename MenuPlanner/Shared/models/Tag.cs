using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MenuPlanner.Shared.Models;

namespace MenuPlanner.Shared.models
{
    public class Tag : Identifier
    {
        [Required]
        public string Name { get; set; }
          
    }
}
