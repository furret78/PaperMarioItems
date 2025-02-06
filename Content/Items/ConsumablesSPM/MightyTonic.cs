using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.ConsumablesSPM
{ 
	public class MightyTonic : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.FoodParticleColors[Type] = [new(156, 107, 173)];
            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults()
        {
            //todo: add status
            Item.DefaultToFood(21, 38, 1500, 0, true);
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(silver: 30);
        }
    }
}
