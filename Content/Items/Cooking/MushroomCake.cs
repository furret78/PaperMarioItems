using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Cooking
{ 
	public class MushroomCake : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.Mushroom;
            ItemID.Sets.FoodParticleColors[Type] = [
                new(255, 219, 140),
                new(198, 146, 99),
                new(132, 77, 49),
                new(255, 199, 99)
            ];
            Item.ResearchUnlockCount = 200;
        }

        public override void SetDefaults()
		{
            Item.DefaultToFood(36, 38, BuffID.WellFed, 14400);
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(silver: 20);
            Item.healLife = 50;
            Item.healMana = 50;
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
            if (sItem.type == PMItemID.MushroomCake) return;
            else orig(player, sItem);
        }

        private static void On_Player_ApplyLifeAndOrMana(On_Player.orig_ApplyLifeAndOrMana orig, Player player, Item sItem)
        {
            if (sItem.type == PMItemID.MushroomCake)
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
	}
}
