using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.CookingSPM
{ 
	public class DayzeeSyrup : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.DayzeeTear;
            ItemID.Sets.FoodParticleColors[Type] = [new(90, 255, 255)];
            Item.ResearchUnlockCount = 80;
        }

        public override void SetDefaults()
        {
            Item.DefaultToFood(39, 40, 0, 0, true);
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 35);
            Item.buffType = PMBuffID.PowerUp;
            Item.buffTime = 2400;
        }

        public override void OnConsumeItem(Player player)
        {
            player.AddBuff(BuffID.Regeneration, 1200);
        }
    }
}
