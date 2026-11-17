using System.Collections.Generic;
using System.Linq;
using RecipeManager.BL;

namespace RecipeManager.DL
{
    public class RecipeRepository
    {
        private readonly List<Recipe> recipes = new List<Recipe>();
        private int nextId = 1;

        public void Add(Recipe recipe)
        {
            recipe.Id = nextId;
            nextId++;
            recipes.Add(recipe);
        }

        public List<Recipe> GetAll()
        {
            return recipes;
        }

        public Recipe? GetById(int id)
        {
            return recipes.FirstOrDefault(r => r.Id == id);
        }

        public void Remove(Recipe recipe)
        {
            recipes.Remove(recipe);
        }
    }
}