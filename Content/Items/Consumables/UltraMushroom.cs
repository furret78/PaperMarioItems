using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class UltraMushroom : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.JamminJelly;
            ItemID.Sets.FoodParticleColors[Type] = [
                new(82, 199, 57),
                new(231, 227, 231),
                new(255, 215, 132)
            ];
            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults()
        {
            Item.DefaultToFood(40, 40, 0, 0);
            Item.rare = ItemRarityID.Green;
            Item.value = Item.buyPrice(gold: 2);
            Item.healLife = 75;
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
        private void On_Player_ApplyPotionDelay(On_Player.orig_ApplyPotionDelay orig, Player self, Item sItem)
        {
            if (sItem.type == Type)
            {
                int delay = 300;
                if (self.pStone) delay = (int)(delay * Player.PhilosopherStoneDurationMultiplier);
                self.AddBuff(21, delay);
            }
            else orig(self, sItem);
        }
        public override void AddRecipes()
		{
            Recipe recipe = CreateRecipe()
                .AddIngredient<SuperMushroom>()
                .AddIngredient(ItemID.HealingPotion, 2)
				.Register();
            recipe = CreateRecipe()
                .AddIngredient<SuperMushroom>()
                .AddIngredient(ItemID.GreaterHealingPotion)
                .Register();
            recipe = CreateRecipe(3)
                .AddRecipeGroup(nameof(ItemID.Mushroom), 9)
                .AddIngredient(ItemID.SuperHealingPotion)
                .Register();
        }
    }
}
