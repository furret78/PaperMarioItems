using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Cooking
{ 
	public class JellySuper : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.SuperMushroom;
            ItemID.Sets.FoodParticleColors[Type] = [
                new(255, 243, 148),
                new(255, 207, 99),
                new(255, 255, 156),
                new(66, 121, 181)
            ];
            Item.ResearchUnlockCount = 125;
        }
        public override void SetDefaults()
		{
            Item.DefaultToFood(33, 39, BuffID.WellFed, 3600);
            Item.value = Item.sellPrice(gold: 1);
            Item.healLife = 50;
            Item.healMana = 75;
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

        private void On_Player_ApplyPotionDelay(On_Player.orig_ApplyPotionDelay orig, Player player, Item sItem)
        {
            if (sItem.type == Type) return;
            else orig(player, sItem);
        }
        private void On_Player_ApplyLifeAndOrMana(On_Player.orig_ApplyLifeAndOrMana orig, Player player, Item sItem)
        {
            if (sItem.type == Type)
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
