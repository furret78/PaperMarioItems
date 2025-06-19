using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.CookingSPM
{ 
	public class DyllisLunch : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.MushroomFry;
            ItemID.Sets.FoodParticleColors[Type] = [
                new(115, 231, 115),
                new(181, 181, 181),
                Color.OrangeRed,
                Color.Orange
            ];
            Item.ResearchUnlockCount = 50;
        }

        public override void SetDefaults()
		{
            Item.DefaultToFood(40, 36, BuffID.WellFed, 7200);
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 75);
            Item.healLife = 60;
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
            if (sItem.type == PMItemID.DyllisLunch) return;
            else orig(player, sItem);
        }
	}
}