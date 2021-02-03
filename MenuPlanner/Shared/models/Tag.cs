using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MenuPlanner.Shared.Models;

namespace MenuPlanner.Shared.models
{
    public class Tag : Entity
    {
      
        public ICollection<Menu> Menus { get; set; }
   
    }
}
