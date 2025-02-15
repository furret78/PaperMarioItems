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
            Item.ResearchUnlockCount = 150;
        }

        public override void SetDefaults()
        {
            Item.DefaultToFood(21, 38, PMBuffID.PowerUp, 300, true);
            Item.rare = ItemRarityID.LightPurple;
            Item.value = Item.sellPrice(silver: 30);
        }
    }
}
