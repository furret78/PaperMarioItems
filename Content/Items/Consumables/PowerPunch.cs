using PaperMarioItems.Content.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class PowerPunch : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 25;
			Item.height = 40;
			Item.useTurn = true;
			Item.useTime = 17;
			Item.useAnimation = Item.useTime;
			Item.useStyle = ItemUseStyleID.DrinkLiquid;
			Item.UseSound = SoundID.Item3;
			Item.consumable = true;
			Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(silver: 15);
			Item.buffType = PMBuffID.Huge;
			Item.buffTime = 14400;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(2)
                .AddIngredient(ItemID.WrathPotion)
                .AddTile(TileID.WorkBenches)
                .Register();
            recipe = CreateRecipe(2)
                .AddIngredient(ItemID.RagePotion)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
