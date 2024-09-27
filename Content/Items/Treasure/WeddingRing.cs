using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Treasure
{ 
	public class WeddingRing : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 34;
			Item.height = 36;
			Item.value = Item.sellPrice(platinum: 1);
			Item.rare = ItemRarityID.Blue;
			Item.accessory = true;
		}
	}
}
