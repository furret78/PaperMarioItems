using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.CookingSPM
{ 
	public class ChocoPastaDish : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.FreshPasta;
            ItemID.Sets.FoodParticleColors[Type] = [
                new(148, 74, 0),
                new(181, 90, 0),
                Color.White
            ];
            Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
		{
            Item.DefaultToFood(40, 32, BuffID.WellFed, 7200);
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(silver: 60);
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
            if (sItem.type == PMItemID.ChocoPasta) return;
            else orig(player, sItem);
        }
	}
}
