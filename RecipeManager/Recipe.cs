using System.Collections.Generic;

namespace RecipeManager.BL
{
    public class Recipe
    {
        public int Id { get; set; }   // assigned by the Data Layer when saved
        public string Name { get; set; }
        public string Category { get; set; }
        public int CookingTimeMinutes { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<string> Steps { get; set; }

        public Recipe(string name, string category, int cookingTime)
        {
            Name = name;
            Category = category;
            CookingTimeMinutes = cookingTime;

            Ingredients = new List<Ingredient>();
            Steps = new List<string>();
        }
    }
}