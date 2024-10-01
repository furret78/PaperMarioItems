using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Cooking
{ 
	public class HeartfulCake : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.CakeMix;
            ItemID.Sets.FoodParticleColors[Type] = [
                new(255, 154, 156),
                new(255, 251, 247),
                new(255, 101, 99)
            ];
            Item.ResearchUnlockCount = 20;
        }
        public override void SetDefaults()
		{
            Item.DefaultToFood(37, 40, BuffID.WellFed, 3600);
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(silver: 10);
            Item.healMana = 100;
        }
        public override void OnConsumeItem(Player player)
		{
            player.AddBuff(PMBuffID.Soft, 72000);
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
