using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class DriedBouquet : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.FoodParticleColors[Type] = [
                new(255, 154, 0),
                new(198, 109, 57),
                new(66, 40, 24)
            ];
            Item.ResearchUnlockCount = 300;
        }

        public override void SetDefaults()
        {
            Item.DefaultToFood(39, 37, 0, 0);
            Item.rare = ItemRarityID.White;
            Item.value = Item.buyPrice(silver: 2);
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
            if (sItem.type == PMItemID.DriedBouquet)
            {
                int delay = (int)(2.5 * 60);
                if (self.pStone) delay = (int)((float)delay * Player.PhilosopherStoneDurationMultiplier);
                self.AddBuff(21, delay);
            }
            else orig(self, sItem);
        }
        public override void AddRecipes()
		{
            Recipe recipe = CreateRecipe()
                .AddRecipeGroup(Language.GetTextValue($"Mods.PaperMarioItems.Content.FlowerGroup"), 3)
                .AddTile(TileID.Furnaces)
				.Register();
            recipe = CreateRecipe()
                .AddRecipeGroup(Language.GetTextValue($"Mods.PaperMarioItems.Content.FlowerGroup"), 3)
                .AddCondition(Condition.NearLava)
                .Register();
            recipe = CreateRecipe()
                .AddRecipeGroup(Language.GetTextValue($"Mods.PaperMarioItems.Content.FlowerGroup"), 3)
                .AddTile(TileID.Campfire)
                .Register();
        }
    }
}
