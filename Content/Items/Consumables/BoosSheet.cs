using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class BoosSheet : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 39;
			Item.useTurn = true;
			Item.useTime = 17;
			Item.useAnimation = Item.useTime;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = PaperMarioItems.useItemPM;
			Item.consumable = true;
			Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(copper: 25);
			Item.buffType = BuffID.Invisibility;
			Item.buffTime = 7200;
        }
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(2)
				.AddIngredient(ItemID.InvisibilityPotion)
				.AddIngredient(ItemID.Silk, 2)
				.AddTile(TileID.WorkBenches)
				.Register();
			//cooking
            recipe = CreateRecipe()
                .AddIngredient<PointSwap>()
                .AddIngredient<RepelCape>()
                .AddCondition(PaperMarioConditions.HasCookbook)
                .AddTile(TileID.CookingPots)
                .Register();
        }
	}
}
