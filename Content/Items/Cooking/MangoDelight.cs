using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Cooking
{ 
	public class MangoDelight : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.Mango;
            ItemID.Sets.FoodParticleColors[Type] = [new(255, 170, 8)];
            Item.ResearchUnlockCount = 25;
        }
        public override void SetDefaults()
		{
            Item.DefaultToFood(40, 39, BuffID.WellFed, 7200);
			Item.UseSound = SoundID.Item3;
			Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(silver: 10);
            Item.healLife = 50;
            Item.healMana = 15;
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
