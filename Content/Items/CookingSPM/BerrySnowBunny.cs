using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.CookingSPM
{ 
	public class BerrySnowBunny : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.SnowBunny;
            ItemID.Sets.FoodParticleColors[Type] = [
                new(255, 66, 198),
                new(255, 206, 206),
            ];
            Item.ResearchUnlockCount = 30;
        }

        public override void SetDefaults()
		{
            Item.DefaultToFood(40, 32, BuffID.WellFed, 3600);
            Item.value = Item.sellPrice(silver: 10);
            Item.healLife = 100;
            Item.potion = true;
        }

        public override void OnConsumeItem(Player player)
		{
            player.AddBuff(BuffID.Frozen, 300);
            player.TryToResetHungerToNeutral();
        }

        public override void Load()
        {
            On_Player.ApplyPotionDelay += On_Player_ApplyPotionDelay;
        }

        private static void On_Player_ApplyPotionDelay(On_Player.orig_ApplyPotionDelay orig, Player player, Item sItem)
        {
            if (sItem.type == PMItemID.BerrySnowBunny) return;
            else orig(player, sItem);
        }
	}
}
