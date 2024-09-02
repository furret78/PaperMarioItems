using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class InnCoupon : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 39;
			Item.height = 40;
			Item.useTurn = true;
			Item.useTime = 17;
			Item.useAnimation = Item.useTime;
			Item.consumable = false;
			Item.maxStack = 1;
            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(silver: 5);
        }
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe()
				.AddIngredient(ItemID.GoldCoin, 60)
				.AddRecipeGroup(RecipeGroupID.Wood)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}
