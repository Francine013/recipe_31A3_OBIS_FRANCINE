using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PF_LAB4_31A3_OBIS_FRANCINE.Data;

namespace PF_LAB4_31A3_OBIS_FRANCINE.Controllers
{
    public class HomeController : Controller
    {
        private readonly RecipeDBContext _context;  

        public HomeController(RecipeDBContext context)  
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var recipes = await _context.Recipes.ToListAsync();
            return View(recipes);
        }

 
        [HttpPut]
        public async Task<IActionResult> UpdateRecipe(int id, [FromBody] Recipe recipe)
        {
            if (id != recipe.Id)
                return Json(new { success = false, message = "ID mismatch" });

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipe);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true, recipe = recipe });
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Json(new { success = false, message = "Recipe not found" });
                }
            }
            return Json(new { success = false, message = "Invalid data" });
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
                return Json(new { success = false, message = "Recipe not found" });

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }


        [HttpGet]
        public async Task<IActionResult> GetRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
                return Json(new { success = false, message = "Recipe not found" });

            return Json(new { success = true, recipe = recipe });
        }
        [HttpPost]
        public async Task<IActionResult> CreateRecipe([FromBody] Recipe recipe)
        {
            try
            {
                Console.WriteLine($"Received recipe: Name={recipe?.Name}, Ingredients={recipe?.Ingredients}");

                if (recipe == null)
                {
                    return Json(new { success = false, message = "Recipe data is null" });
                }

                if (string.IsNullOrEmpty(recipe.Name))
                {
                    return Json(new { success = false, message = "Recipe name is required" });
                }

                if (string.IsNullOrEmpty(recipe.Ingredients))
                {
                    return Json(new { success = false, message = "Ingredients are required" });
                }


                try
                {
                    System.Text.Json.JsonDocument.Parse(recipe.Ingredients);
                }
                catch (System.Text.Json.JsonException ex)
                {
                    return Json(new { success = false, message = $"Invalid ingredients JSON format: {ex.Message}" });
                }

                recipe.Id = 0;

                _context.Recipes.Add(recipe);
                await _context.SaveChangesAsync();

                return Json(new { success = true, recipe = recipe });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateRecipe: {ex.Message}");
                return Json(new { success = false, message = $"Server error: {ex.Message}" });
            }
        }
    }
}