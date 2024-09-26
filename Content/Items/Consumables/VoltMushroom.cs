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
        public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 40;
			Item.useTurn = true;
			Item.useTime = 17;
			Item.useAnimation = Item.useTime;
			Item.useStyle = ItemUseStyleID.EatFood;
			Item.UseSound = SoundID.Item2;
			Item.consumable = true;
			Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(silver: 10);
			Item.buffType = PMBuffID.Electrified;
			Item.buffTime = effectTime*60*60;
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
