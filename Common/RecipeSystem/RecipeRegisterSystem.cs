using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Common.RecipeSystem
{
    /// <summary>
    /// Data structure that stores the necessary data for a recipe to be added to the register.
    /// Prehardmode-exclusive recipes must come after Hardmode-exclusives if they share the same ingredients.
    /// </summary>
    /// <param name="result">The resulting item</param>
    /// <param name="item1">The first ingredient in the recipe</param>
    /// <param name="item2">The second, optional ingredient in the recipe. Defaults to <see cref="ItemID.None"/></param>
    /// <param name="hardmode">Setting this to true means the recipe is only available in Hardmode. Defaults to false.</param>
    /// <param name="prehardmode">Setting this to true means the recipe is only available in Pre-Hardmode. Defaults to false.</param>
    public struct PMRecipe(int result, int item1, int item2 = ItemID.None, bool hardmode = false, bool prehardmode = false)
    {
        public int ResultingItem = result, Ingredient1 = item1, Ingredient2 = item2;
        public bool Hardmode = hardmode, Prehard = prehardmode;
    }

    public class RecipeRegister : ModSystem
	{
        /// <summary>
        /// A recipe Dictionary that stores all item combinations except for Mystery Box or Space Food combinations
        /// </summary>
        public static Dictionary<int, PMRecipe> MainRecipeDictionary = new();

        /// <summary>
        /// A recipe List exclusively dedicated to Mystery Box cooking
        /// </summary>
        public static List<int> MysteryBoxRecipeList = new();

        /// <summary>
        /// A recipe List specifying what items should make Space Food when combined with Dried Bouquet
        /// </summary>
        public static List<int> SpaceFoodList = new();

        /// <summary>
        /// A recipe List specifying what items will NOT make Space Food when combined with Dried Bouquet (takes precedent over the whitelist)
        /// </summary>
        public static List<int> SpaceFoodBlacklist = new();

        public override void Unload()
        {
            MainRecipeDictionary = null;
        }
    }
}