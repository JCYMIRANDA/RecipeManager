using RecipeManager.BL;
using RecipeManager.DL;
using RecipeManager.UI;

namespace RecipeManager
{
    class Program
    {
        static void Main()
        {
            RecipeRepository repository = new RecipeRepository();   // DL
            RecipeService service = new RecipeService(repository);  // BL
            RecipeUI ui = new RecipeUI(service);                    // UI

            ui.Run();
        }
    }
}