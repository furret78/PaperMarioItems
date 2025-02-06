using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.ConsumablesSPM
{ 
	public class SlimyMushroom : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.Apple;
            ItemID.Sets.FoodParticleColors[Type] = [
                new(255, 165, 165),
                new(231, 90, 132)
            ];
            Item.ResearchUnlockCount = 75;
        }

        public override void SetDefaults()
        {
            Item.DefaultToFood(33, 36, 600, BuffID.WellFed);
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(silver: 20);
            Item.healLife = 100;
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
