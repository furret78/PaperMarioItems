using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.CookingSPM
{ 
	public class AwesomeSnack : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.CakeMix;
            ItemID.Sets.FoodParticleColors[Type] = [
                new(255, 74, 0),
                new(255, 156, 0)
            ];
            Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
		{
            Item.DefaultToFood(38, 40, BuffID.WellFed, 7200);
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(silver: 25);
            Item.healLife = 25;
            Item.potion = true;
        }

        public override void OnConsumeItem(Player player)
		{
            player.TryToResetHungerToNeutral();
            player.ClearBuff(BuffID.Poisoned);
        }

        public override void Load()
        {
            On_Player.ApplyPotionDelay += On_Player_ApplyPotionDelay;
        }

        private static void On_Player_ApplyPotionDelay(On_Player.orig_ApplyPotionDelay orig, Player player, Item sItem)
        {
            if (sItem.type == PMItemID.AwesomeSnack) return;
            else orig(player, sItem);
        }
	}
}
