using System;
using System.Collections.Generic;
using RecipeManager.BL;

namespace RecipeManager.UI
{
    public class RecipeUI
    {
        private readonly RecipeService service;

        public RecipeUI(RecipeService service)
        {
            this.service = service;
        }

        // ---------- Display helpers ----------
        private void DisplayRecipe(Recipe recipe)
        {
            Console.WriteLine();
            Console.WriteLine("========== " + recipe.Name + " ==========");
            Console.WriteLine("ID: " + recipe.Id);
            Console.WriteLine("Category: " + recipe.Category);
            Console.WriteLine("Cooking Time: " + recipe.CookingTimeMinutes + " minutes");

            Console.WriteLine("\n** Ingredients **");
            if (recipe.Ingredients.Count == 0)
            {
                Console.WriteLine("No ingredients added.");
            }
            else
            {
                foreach (Ingredient ingredient in recipe.Ingredients)
                {
                    Console.WriteLine(ingredient);
                }
            }

            Console.WriteLine("\n** Steps **");
            if (recipe.Steps.Count == 0)
            {
                Console.WriteLine("No steps added.");
            }
            else
            {
                for (int i = 0; i < recipe.Steps.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + recipe.Steps[i]);
                }
            }

            Console.WriteLine("============================");
        }

        // ---------- Input helpers ----------
        private string ReadRequiredText(string prompt, string errorMessage)
        {
            Console.Write(prompt);
            string value = Console.ReadLine() ?? "";

            while (!service.IsValidText(value))
            {
                Console.Write(errorMessage);
                value = Console.ReadLine() ?? "";
            }

            return value;
        }

        private int ReadPositiveInt(string prompt)
        {
            int number;
            while (true)
            {
                Console.Write(prompt);

                if (int.TryParse(Console.ReadLine(), out number) && service.IsValidPositive(number))
                {
                    return number;
                }

                Console.WriteLine("Please enter a valid positive number.");
            }
        }

        private double ReadPositiveDouble(string prompt)
        {
            double number;
            while (true)
            {
                Console.Write(prompt);

                if (double.TryParse(Console.ReadLine(), out number) && service.IsValidPositive(number))
                {
                    return number;
                }

                Console.WriteLine("Please enter a valid positive number.");
            }
        }

        // ---------- Menu actions ----------
        private void AddRecipe()
        {
            string name = ReadRequiredText("\nRecipe Name: ", "Recipe name cannot be empty. Enter again: ");
            string category = ReadRequiredText("Category: ", "Category cannot be empty. Enter again: ");
            int time = ReadPositiveInt("Cooking Time (minutes): ");

            Recipe newRecipe = service.CreateRecipe(name, category, time);

            Console.WriteLine("\nAdd Ingredients.");
            Console.WriteLine("Type 'done' when finished.");

            while (true)
            {
                Console.Write("Ingredient Name: ");
                string ingName = Console.ReadLine() ?? "";

                if (ingName.Equals("done", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                if (!service.IsValidText(ingName))
                {
                    Console.WriteLine("Ingredient name cannot be empty.");
                    continue;
                }

                double quantity = ReadPositiveDouble("Quantity: ");
                string unit = ReadRequiredText("Unit (cup, tsp, kg, etc.): ", "Unit cannot be empty. Enter again: ");

                newRecipe.Ingredients.Add(service.CreateIngredient(ingName, quantity, unit));
            }

            Console.WriteLine("\nAdd Steps.");
            Console.WriteLine("Type 'done' when finished.");

            int stepNum = 1;
            while (true)
            {
                Console.Write("Step " + stepNum + ": ");
                string step = Console.ReadLine() ?? "";

                if (step.Equals("done", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                if (!service.IsValidText(step))
                {
                    Console.WriteLine("Step cannot be empty.");
                    continue;
                }

                newRecipe.Steps.Add(step);
                stepNum++;
            }

            service.SaveRecipe(newRecipe);

            Console.WriteLine("\nRecipe '" + name + "' added successfully!");
        }

        private void ViewAllRecipes()
        {
            Console.WriteLine("\n========== ALL RECIPES ==========");

            List<Recipe> recipes = service.GetAllRecipes();

            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes yet.");
                return;
            }

            foreach (Recipe recipe in recipes)
            {
                Console.WriteLine(recipe.Id + ". " + recipe.Name + " - " +
                    recipe.Category + " - " + recipe.CookingTimeMinutes + " mins");
            }

            Console.WriteLine("=================================");
        }

        private void SearchRecipe()
        {
            Console.Write("\nSearch by Name or Category: ");
            string keyword = Console.ReadLine() ?? "";

            if (!service.IsValidText(keyword))
            {
                Console.WriteLine("Search keyword cannot be empty.");
                return;
            }

            List<Recipe> results = service.SearchRecipes(keyword);

            if (results.Count == 0)
            {
                Console.WriteLine("No recipe found.");
                return;
            }

            Console.WriteLine("\nSearch Results:");
            foreach (Recipe recipe in results)
            {
                DisplayRecipe(recipe);
            }
        }

        private void ViewRecipeDetails()
        {
            Console.Write("\nEnter Recipe ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID. Please enter a number.");
                return;
            }

            Recipe? recipe = service.GetRecipeById(id);

            if (recipe != null)
            {
                DisplayRecipe(recipe);
            }
            else
            {
                Console.WriteLine("Recipe not found.");
            }
        }

        private void DeleteRecipe()
        {
            Console.Write("\nEnter Recipe ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID. Please enter a number.");
                return;
            }

            Recipe? deleted = service.DeleteRecipe(id);

            if (deleted != null)
            {
                Console.WriteLine("Recipe '" + deleted.Name + "' deleted successfully.");
            }
            else
            {
                Console.WriteLine("Recipe not found.");
            }
        }

        // ---------- Main menu loop ----------
        public void Run()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("================================");
                Console.WriteLine("        RECIPE MANAGER");
                Console.WriteLine("================================");
                Console.WriteLine("1. Add Recipe");
                Console.WriteLine("2. View All Recipes");
                Console.WriteLine("3. Search Recipe");
                Console.WriteLine("4. View Recipe Details");
                Console.WriteLine("5. Delete Recipe");
                Console.WriteLine("6. Exit");
                Console.WriteLine("================================");

                Console.Write("Choose an option: ");
                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1": AddRecipe(); break;
                    case "2": ViewAllRecipes(); break;
                    case "3": SearchRecipe(); break;
                    case "4": ViewRecipeDetails(); break;
                    case "5": DeleteRecipe(); break;
                    case "6":
                        Console.WriteLine("Thank you for using Recipe Manager!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please select 1-6.");
                        break;
                }

                Console.WriteLine("\nPress ENTER to continue...");
                Console.ReadLine();
            }
        }
    }
}