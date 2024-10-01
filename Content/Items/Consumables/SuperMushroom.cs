using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class SuperMushroom : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.MapleSyrup;
            ItemID.Sets.FoodParticleColors[Type] = [
                new(41, 138, 214),
                new(231, 227, 231),
                new(255, 215, 132)
            ];
            Item.ResearchUnlockCount = 150;
        }

        public override void SetDefaults()
        {
            Item.DefaultToFood(40, 40, 0, 0);
            Item.value = Item.sellPrice(silver: 5);
            Item.healLife = 50;
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
                .AddIngredient<Mushroom>()
                .AddIngredient(ItemID.LesserHealingPotion, 3)
                .AddTile(TileID.WorkBenches)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient<Mushroom>()
                .AddIngredient(ItemID.HealingPotion)
                .AddTile(TileID.WorkBenches)
                .Register();
            recipe = CreateRecipe()
                .AddRecipeGroup(nameof(ItemID.Mushroom), 3)
                .AddIngredient(ItemID.HealingPotion)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
