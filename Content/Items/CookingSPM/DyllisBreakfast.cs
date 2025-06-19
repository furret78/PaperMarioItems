using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.CookingSPM
{ 
	public class DyllisBreakfast : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.MushroomShake;
            ItemID.Sets.FoodParticleColors[Type] = [
                new(181, 107, 24),
                new(99, 222, 99)
            ];
            Item.ResearchUnlockCount = 50;
        }

        public override void SetDefaults()
		{
            Item.DefaultToFood(40, 32, BuffID.WellFed, 7200);
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 60);
            Item.healLife = 40;
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
            if (sItem.type == PMItemID.DyllisBreakfast) return;
            else orig(player, sItem);
        }
	}
}