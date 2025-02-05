using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.ConsumablesSPM
{ 
	public class OrangeApple : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.Apple;
            ItemID.Sets.FoodParticleColors[Type] = [
                new(255, 198, 90),
                new(255, 123, 0),
                new(255, 214, 24)
            ];
            Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            Item.DefaultToFood(35, 38, 0, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(silver: 15);
            Item.healLife = 25;
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
