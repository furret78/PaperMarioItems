using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using PaperMarioItems.Content.Items.Consumables;

namespace PaperMarioItems.Content.Items.Cooking
{ 
	public class MushroomRoast : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.FoodParticleColors[Type] = [
                new(207, 151, 75),
                new(121, 75, 21)
            ];
            Item.ResearchUnlockCount = 120;
        }

        public override void SetDefaults()
		{
            Item.DefaultToFood(40, 38, BuffID.WellFed2, 14400);
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(silver: 15);
            Item.healLife = 75;
            Item.healMana = 25;
            Item.potion = true;
        }

        public override void OnConsumeItem(Player player)
		{
            player.TryToResetHungerToNeutral();
        }

        public override void Load()
        {
            On_Player.ApplyPotionDelay += On_Player_ApplyPotionDelay;
            On_Player.ApplyLifeAndOrMana += On_Player_ApplyLifeAndOrMana;
        }

        private static void On_Player_ApplyPotionDelay(On_Player.orig_ApplyPotionDelay orig, Player player, Item sItem)
        {
            if (sItem.type == PMItemID.MushroomRoast) return;
            else orig(player, sItem);
        }

        private static void On_Player_ApplyLifeAndOrMana(On_Player.orig_ApplyLifeAndOrMana orig, Player player, Item sItem)
        {
            if (sItem.type == PMItemID.MushroomRoast)
            {
                int num = sItem.healLife; int num2 = sItem.healMana;
                player.statLife += num; player.statMana += num2;
                if (player.statLife > player.statLifeMax2) player.statLife = player.statLifeMax2;
                if (player.statMana > player.statManaMax2) player.statMana = player.statManaMax2;
                if (num > 0 && Main.myPlayer == player.whoAmI) player.HealEffect(num);
                if (num2 > 0)
                {
                    if (Main.myPlayer == player.whoAmI) player.ManaEffect(num2);
                }
            }
            else orig(player, sItem);
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe()
                .AddIngredient<SlowMushroom>()
                .AddTile(TileID.CookingPots)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient<LifeMushroom>()
                .AddTile(TileID.CookingPots)
                .Register();
        }
	}
}
