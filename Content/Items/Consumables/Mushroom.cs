using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class Mushroom : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.HoneySyrup;
            ItemID.Sets.FoodParticleColors[Type] = [
                new(231, 56, 57),
                new(231, 227, 231),
                new(255, 215, 132)
            ];
            Item.ResearchUnlockCount = 200;
        }

        public override void SetDefaults()
        {
            Item.DefaultToFood(40, 40, 0, 0);
            Item.rare = ItemRarityID.White;
            Item.value = Item.buyPrice(silver: 5);
            Item.healLife = 25;
            Item.potion = true;
        }
        public override void OnConsumeItem(Player player)
        {
            player.TryToResetHungerToNeutral();
        }
        //detour
        public override void Load()
        {
            On_Player.ApplyPotionDelay += On_Player_ApplyPotionDelay;
        }
        private static void On_Player_ApplyPotionDelay(On_Player.orig_ApplyPotionDelay orig, Player self, Item sItem)
        {
            if (sItem.type == PMItemID.Mushroom)
            {
                int delay = 300;
                if (self.pStone) delay = (int)(delay * Player.PhilosopherStoneDurationMultiplier);
                self.AddBuff(21, delay);
            }
            else orig(self, sItem);
        }
        public override void AddRecipes()
		{
            Recipe recipe = CreateRecipe(2)
                .AddRecipeGroup(nameof(ItemID.Mushroom))
                .AddIngredient(ItemID.LesserHealingPotion)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
