using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Common.RecipeSystem
{
    /// <summary>
    /// Data structure that stores the necessary data for a recipe to be added to the register.
    /// </summary>
    /// <param name="result">The resulting item</param>
    /// <param name="item1">The first ingredient in the recipe</param>
    /// <param name="item2">The second, optional ingredient in the recipe. Defaults to ItemID.None</param>
    /// <param name="hardmode">Setting this to true means the recipe is only available in Hardmode. Defaults to false</param>
    /// <param name="prehardmode">Setting this to true means the recipe is only available in Pre-Hardmode. Defaults to false</param>
    public struct PMRecipe(int result, int item1, int item2 = ItemID.None, bool hardmode = false, bool prehardmode = false)
    {
        public int ResultingItem = result, Ingredient1 = item1, Ingredient2 = item2;
        public bool Hardmode = hardmode, Prehard = prehardmode;
    }

    public class RecipeRegister : ModSystem
	{
        public static Dictionary<int, PMRecipe> MainRecipeDictionary = new();

        public override void Unload()
        {
            MainRecipeDictionary = null;
        }
    }
}