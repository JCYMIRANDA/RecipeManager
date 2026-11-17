using RecipeManager.DL;
using RecipeManager.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeManager.BL
{
    public class RecipeService
    {
        private readonly RecipeRepository repository;

        public RecipeService(RecipeRepository repository)
        {
            this.repository = repository;
        }

        // ---------- Validation rules ----------
        public bool IsValidText(string? text)
        {
            return !string.IsNullOrWhiteSpace(text);
        }

        public bool IsValidPositive(double value)
        {
            return value > 0;
        }

        // ---------- Creating objects ----------
        public Recipe CreateRecipe(string name, string category, int cookingTime)
        {
            return new Recipe(name, category, cookingTime);
        }

        public Ingredient CreateIngredient(string name, double quantity, string unit)
        {
            return new Ingredient(name, quantity, unit);
        }

        // ---------- Operations ----------
        public void SaveRecipe(Recipe recipe)
        {
            repository.Add(recipe);
        }

        public List<Recipe> GetAllRecipes()
        {
            return repository.GetAll();
        }

        public Recipe? GetRecipeById(int id)
        {
            return repository.GetById(id);
        }

        public List<Recipe> SearchRecipes(string keyword)
        {
            return repository.GetAll()
                .Where(r =>
                    r.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    r.Category.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public Recipe? DeleteRecipe(int id)
        {
            Recipe? recipe = repository.GetById(id);

            if (recipe != null)
            {
                repository.Remove(recipe);
            }

            return recipe;
        }
    }
}