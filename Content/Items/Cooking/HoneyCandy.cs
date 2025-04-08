using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Cooking
{ 
	public class HoneyCandy : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.HoneySyrup;
            ItemID.Sets.FoodParticleColors[Type] = [
                new(231, 231, 0),
                new(255, 154, 0)
            ];
            Item.ResearchUnlockCount = 200;
        }

        public override void SetDefaults()
		{
            Item.DefaultToFood(40, 40, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(silver: 15);
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

        private static void On_Player_ApplyLifeAndOrMana(On_Player.orig_ApplyLifeAndOrMana orig, Player player, Item sItem)
        {
            if (sItem.type == PMItemID.HoneyCandy)
            {
                int num2 = sItem.healMana;
                player.statMana += num2;
                if (player.statMana > player.statManaMax2) player.statMana = player.statManaMax2;
                if (num2 > 0)
                {
                    if (Main.myPlayer == player.whoAmI) player.ManaEffect(num2);
                }
            }
            else orig(player, sItem);
        }
	}
}
