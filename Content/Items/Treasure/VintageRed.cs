using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Treasure
{ 
	public class VintageRed : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.TastyTonic;
            ItemID.Sets.DrinkParticleColors[Type] = [new(237, 70, 79)];
        }

        public override void SetDefaults()
		{
			Item.DefaultToFood(30, 40, BuffID.Tipsy, 36000, true);
			Item.value = Item.sellPrice(silver: 20);
			Item.rare = ItemRarityID.LightRed;
		}
	}
}