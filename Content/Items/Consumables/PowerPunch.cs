using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class PowerPunch : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.DrinkParticleColors[Type] = [new(255, 243, 181)];
            Item.ResearchUnlockCount = 50;
        }

        public override void SetDefaults()
		{
            Item.DefaultToFood(25, 40, PMBuffID.Huge, 14400, true);
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(silver: 15);
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
