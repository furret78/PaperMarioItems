using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class SlowMushroom : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.FoodParticleColors[Type] = [
                new(255, 138, 156),
                new(90, 36, 66),
                new(224, 89, 109),
                new(255, 215, 132)
            ];
            Item.ResearchUnlockCount = 200;
        }

        public override void SetDefaults()
		{
            Item.DefaultToFood(40, 40, BuffID.Regeneration, 36000);
            Item.rare = ItemRarityID.White;
            Item.value = Item.buyPrice(copper: 15);
        }

        public override void OnConsumeItem(Player player)
        {
            player.TryToResetHungerToNeutral();
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe()
				.AddRecipeGroup(nameof(ItemID.Mushroom), 3)
                .AddIngredient(ItemID.RegenerationPotion)
                .AddTile(TileID.WorkBenches)
                .Register();
			recipe = CreateRecipe(2)
				.AddIngredient<Mushroom>(2)
                .AddIngredient(ItemID.RegenerationPotion)
                .AddTile(TileID.WorkBenches)
                .Register();
            recipe = CreateRecipe(3)
                .AddIngredient<SuperMushroom>()
                .AddIngredient(ItemID.RegenerationPotion)
                .AddTile(TileID.WorkBenches)
                .Register();
            recipe = CreateRecipe(5)
                .AddIngredient<UltraMushroom>()
                .AddIngredient(ItemID.RegenerationPotion)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
	}
}
