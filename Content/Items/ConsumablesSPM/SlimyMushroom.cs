using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.ConsumablesSPM
{ 
	public class SlimyMushroom : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.FoodParticleColors[Type] = [
                new(255, 165, 165),
                new(231, 90, 132),
            ];
            Item.ResearchUnlockCount = 75;
        }

        public override void SetDefaults()
        {
            Item.DefaultToFood(33, 36, BuffID.WellFed, 600);
            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(silver: 20);
            Item.healLife = 100;
            Item.potion = true;
        }

        public override void OnConsumeItem(Player player)
        {
            player.TryToResetHungerToNeutral();
            player.ClearBuff(BuffID.Poisoned);
        }

        //detour
        public override void Load()
        {
            On_Player.ApplyPotionDelay += On_Player_ApplyPotionDelay;
        }

        private void On_Player_ApplyPotionDelay(On_Player.orig_ApplyPotionDelay orig, Player self, Item sItem)
        {
            if (sItem.type == Type)
            {
                int delay = 600;
                if (self.pStone) delay = (int)(delay * Player.PhilosopherStoneDurationMultiplier);
                self.AddBuff(21, delay);
            }
            else orig(self, sItem);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe()
                .AddIngredient(PMItemID.Mushroom)
                .AddIngredient(ItemID.Gel, 3)
                .AddTile(TileID.CookingPots)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient(ItemID.GlowingMushroom, 3)
                .AddIngredient(ItemID.Gel, 3)
                .AddTile(TileID.CookingPots)
                .Register();
        }
    }
}