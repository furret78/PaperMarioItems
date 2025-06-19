using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.CookingSPM
{ 
	public class DyllisDinner : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.MushroomFry;
            ItemID.Sets.FoodParticleColors[Type] = [
                new(206, 206, 206),
                new(115, 231, 115),
                new(255, 99, 99),
                Color.Red
            ];
            Item.ResearchUnlockCount = 70;
        }

        public override void SetDefaults()
		{
            Item.DefaultToFood(40, 38, BuffID.WellFed, 7200);
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(gold: 1);
            Item.healLife = 100;
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
            if (sItem.type == PMItemID.DyllisDinner) return;
            else orig(player, sItem);
        }
	}
}