using System.Collections.Generic;
using Terraria.ModLoader;

namespace PaperMarioItems.Common.RecipeSystem
{
    public struct PMRecipe(int result, int item1, int item2)
    {
        public int ResultingItem = result, Ingredient1 = item1, Ingredient2 = item2;
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