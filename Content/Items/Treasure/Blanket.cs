using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Treasure
{ 
	public class Blanket : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 35;
			Item.value = Item.sellPrice(silver: 2);
			Item.rare = ItemRarityID.Orange;
		}
	}
}
