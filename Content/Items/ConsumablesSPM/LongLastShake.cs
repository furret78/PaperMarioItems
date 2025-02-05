using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.ConsumablesSPM
{ 
	public class LongLastShake : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.FoodParticleColors[Type] = [
                new(255, 214, 132),
                new(231, 231, 231),
                new(255, 156, 0)
            ];
            Item.ResearchUnlockCount = 180;
        }

        public override void SetDefaults()
        {
            Item.DefaultToFood(31, 39, BuffID.Regeneration, 36000, true);
            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(copper: 50, silver: 11);
        }

        public override void OnConsumeItem(Player player)
        {
            player.TryToResetHungerToNeutral();
        }

        public override void AddRecipes()
		{
            Recipe recipe = CreateRecipe()
                .AddIngredient(PMItemID.SlowMushroom)
                .AddTile(TileID.Bottles)
                .AddCondition(Condition.NearWater)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient(PMItemID.SlowMushroom)
                .AddIngredient(ItemID.BottledWater)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
