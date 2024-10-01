using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class VoltMushroom : ModItem
	{
        private int effectTime = 3;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(effectTime);

        public override void SetStaticDefaults()
        {
            ItemID.Sets.FoodParticleColors[Type] = [
                new(8, 48, 74),
                new(255, 190, 0),
                new(255, 215, 132)
            ];
            Item.ResearchUnlockCount = 120;
        }

        public override void SetDefaults()
		{
            Item.DefaultToFood(40, 40, PMBuffID.Electrified, effectTime * 60 * 60);
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(silver: 10);
        }

        public override void OnConsumeItem(Player player)
        {
            player.TryToResetHungerToNeutral();
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.LightYellow.ToVector3() * 0.55f * Main.essScale);
        }

        public override void AddRecipes()
		{
            Recipe recipe = CreateRecipe()
               	.AddRecipeGroup(nameof(ItemID.Mushroom), 3)
                .AddIngredient(ItemID.Wire, 3)
				.AddTile(TileID.WorkBenches)
				.Register();
			recipe = CreateRecipe(2)
				.AddIngredient<Mushroom>(2)
                .AddIngredient(ItemID.Wire, 3)
                .AddTile(TileID.WorkBenches)
                .Register();
            recipe = CreateRecipe(3)
                .AddIngredient<SuperMushroom>()
                .AddIngredient(ItemID.Wire, 3)
                .AddTile(TileID.WorkBenches)
                .Register();
            recipe = CreateRecipe(5)
                .AddIngredient<UltraMushroom>()
                .AddIngredient(ItemID.Wire, 3)
                .AddTile(TileID.WorkBenches)
                .Register();
            //cloud in a bottle ones
            recipe = CreateRecipe()
                .AddRecipeGroup(nameof(ItemID.Mushroom), 3)
                .AddIngredient(ItemID.CloudinaBottle)
                .AddTile(TileID.WorkBenches)
                .Register();
            recipe = CreateRecipe(3)
                .AddIngredient<Mushroom>()
                .AddIngredient(ItemID.CloudinaBottle)
                .AddTile(TileID.WorkBenches)
                .Register();
            recipe = CreateRecipe(5)
                .AddIngredient<SuperMushroom>()
                .AddIngredient(ItemID.CloudinaBottle)
                .AddTile(TileID.WorkBenches)
                .Register();
            recipe = CreateRecipe(7)
                .AddIngredient<UltraMushroom>()
                .AddIngredient(ItemID.CloudinaBottle)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
	}
}
