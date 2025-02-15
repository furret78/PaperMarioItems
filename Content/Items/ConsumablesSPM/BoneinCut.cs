using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.ConsumablesSPM
{ 
	public class BoneinCut : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.Steak;
            ItemID.Sets.FoodParticleColors[Type] = [
                new(255, 107, 107),
                new(255, 165, 165),
                new(231, 90, 132),
            ];
            Item.ResearchUnlockCount = 150;
        }

        public override void SetDefaults()
        {
            Item.DefaultToFood(40, 40, PMBuffID.PowerUp, 1200);
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(silver: 30);
        }

        public override void OnConsumeItem(Player player)
        {
            player.TryToResetHungerToNeutral();
            player.AddBuff(BuffID.WellFed, 7200);
            player.ClearBuff(BuffID.Poisoned);
        }
    }
}