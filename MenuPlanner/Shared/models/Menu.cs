using System;
using System.Collections.Generic;

namespace MenuPlanner.Shared.models
{
    public class Menu
    {
        public Guid Id;
        public string Name;
        public int TimeOfDay; //Flags
        public int Season; //Flags
        public string Description;
        public List<string> Steps;
        public Dictionary<Ingredient,Quantity> Ingredients;
        public List<Comment> Comments;
        public double AverageRating;
        public int Votes;
        
        public int MenuCategory; //Flags
        // Ev on Ingredient
        public int Calories;
        public double Price;
        // End
        public List<Image> Images;
        public Uri Video;
    }
}