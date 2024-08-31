using PaperMarioItems.Content.Items;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace PaperMarioItems.Common.RecipeSystem
{
    public class RecipeList : ModSystem
    {
        //first value is the result, second is the ingredient #1, third is the ingredient #2
        public override void SetStaticDefaults()
        {
            var IngredientList = new List<PMRecipe>()
            {
                new(PMItemID.BoosSheet, PMItemID.PointSwap, PMItemID.RepelCape),
                new(PMItemID.Koopasta, PMItemID.FreshPasta, PMItemID.TurtleyLeaf),
                new(PMItemID.SpicyPasta, PMItemID.Spaghetti, PMItemID.HotSauce)
            };

            for (int i = 0; i < IngredientList.Count; i++)
            {
                RecipeRegister.MainRecipeDictionary.Add(i, IngredientList[i]);
            }
        }
    }
}