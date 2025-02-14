using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.ConsumablesSPM
{
    public class PrimordialFruit : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.Apple;
            ItemID.Sets.FoodParticleColors[Type] = [
                new(255, 189, 0),
                new(214, 16, 181),
                new(66, 16, 214),
                new(255, 123, 0),
                Color.Red
            ];
            Item.ResearchUnlockCount = 75;
        }

        public override void SetDefaults()
        {
            Item.DefaultToFood(34, 39, BuffID.WellFed, 1800);
            Item.rare = ItemRarityID.Blue;
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