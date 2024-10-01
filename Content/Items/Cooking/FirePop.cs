using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Cooking
{ 
	public class FirePop : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.FireFlower;
            ItemID.Sets.FoodParticleColors[Type] = [
                new(255, 231, 148),
                new(206, 60, 57),
                new(159, 46, 38)
            ];
            Item.ResearchUnlockCount = 50;
        }
        public override void SetDefaults()
		{
            Item.DefaultToFood(39, 39, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(silver: 20);
            Item.healMana = 100;
        }
        public override void OnConsumeItem(Player player)
		{
            player.TryToResetHungerToNeutral();
        }
        public override void Load()
        {
            On_Player.ApplyLifeAndOrMana += On_Player_ApplyLifeAndOrMana;
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
