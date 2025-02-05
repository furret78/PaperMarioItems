using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.ConsumablesSPM
{ 
	public class BlackApple : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.Apple;
            ItemID.Sets.FoodParticleColors[Type] = [
                new(82, 82, 82),
                new(8, 8, 8),
                new(123, 123, 123)
            ];
            Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            Item.DefaultToFood(35, 38, 0, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(silver: 1);
            Item.healLife = 5;
            Item.potion = true;
        }

        public override void OnConsumeItem(Player player)
        {
            player.TryToResetHungerToNeutral();
            player.ClearBuff(BuffID.Poisoned);
        }

        //detour
        public override void Load()
        {
            On_Player.ApplyPotionDelay += On_Player_ApplyPotionDelay;
        }

        private void On_Player_ApplyPotionDelay(On_Player.orig_ApplyPotionDelay orig, Player self, Item sItem)
        {
            if (sItem.type == Type) return;
            else orig(self, sItem);
        }
    }
}
