using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Cooking
{ 
	public class ZessDinner : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.SuperMushroom;
            ItemID.Sets.FoodParticleColors[Type] = [
                new(222, 223, 222),
                new(255, 154, 156),
                new(231, 60, 0),
                new(148, 255, 156)
            ];
            Item.ResearchUnlockCount = 120;
        }

        public override void SetDefaults()
		{
            Item.DefaultToFood(40, 39, BuffID.WellFed2, 18000);
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(silver: 10);
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
            if (sItem.type == PMItemID.ZessDinner) return;
            else orig(player, sItem);
        }

        private static void On_Player_ApplyLifeAndOrMana(On_Player.orig_ApplyLifeAndOrMana orig, Player player, Item sItem)
        {
            if (sItem.type == PMItemID.ZessDinner)
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
