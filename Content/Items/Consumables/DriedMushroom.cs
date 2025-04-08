using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class DriedMushroom : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.FoodParticleColors[Type] = [
                new(231, 182, 181),
                new(165, 81, 82),
                new(255, 207, 99)
            ];
            Item.ResearchUnlockCount = 300;
        }

        public override void SetDefaults()
        {
            Item.DefaultToFood(33, 38, 0, 0);
            Item.rare = ItemRarityID.White;
            Item.value = Item.buyPrice(copper: 50);
            Item.healLife = 5;
            Item.potion = true;
        }
        public override void OnConsumeItem(Player player)
        {
            player.TryToResetHungerToNeutral();
        }
        public override void Load()
        {
            On_Player.ApplyPotionDelay += On_Player_ApplyPotionDelay;
        }

        private static void On_Player_ApplyPotionDelay(On_Player.orig_ApplyPotionDelay orig, Player self, Item sItem)
        {
            if (sItem.type == PMItemID.DriedMushroom)
            {
                int delay = 300;
                if (self.pStone) delay = (int)((float)delay * Player.PhilosopherStoneDurationMultiplier);
                self.AddBuff(21, delay);
            }
            else orig(self, sItem);
        }

        public override void AddRecipes()
		{
            //furnaces
            Recipe recipe = CreateRecipe()
                .AddRecipeGroup(nameof(ItemID.Mushroom))
                .AddTile(TileID.Furnaces)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient<Mushroom>()
                .AddTile(TileID.Furnaces)
                .Register();
            recipe = CreateRecipe(3)
                .AddIngredient<SuperMushroom>()
                .AddTile(TileID.Furnaces)
                .Register();
            recipe = CreateRecipe(5)
                .AddIngredient<UltraMushroom>()
                .AddTile(TileID.Furnaces)
                .Register();
            //campfire
            recipe = CreateRecipe()
                .AddRecipeGroup(nameof(ItemID.Mushroom))
                .AddTile(TileID.Campfire)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient<Mushroom>()
                .AddTile(TileID.Campfire)
                .Register();
            recipe = CreateRecipe(3)
                .AddIngredient<SuperMushroom>()
                .AddTile(TileID.Campfire)
                .Register();
            recipe = CreateRecipe(5)
                .AddIngredient<UltraMushroom>()
                .AddTile(TileID.Campfire)
                .Register();
            //near lava
            recipe = CreateRecipe()
                .AddRecipeGroup(nameof(ItemID.Mushroom))
                .AddCondition(Condition.NearLava)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient<Mushroom>()
                .AddCondition(Condition.NearLava)
                .Register();
            recipe = CreateRecipe(3)
                .AddIngredient<SuperMushroom>()
                .AddCondition(Condition.NearLava)
                .Register();
            recipe = CreateRecipe(5)
                .AddIngredient<UltraMushroom>()
                .AddCondition(Condition.NearLava)
                .Register();
        }
    }
}
