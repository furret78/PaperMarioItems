using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Treasure
{ 
	public class GoldRing : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 31;
			Item.height = 33;
			Item.value = Item.sellPrice(gold: 50);
			Item.rare = ItemRarityID.Yellow;
			Item.accessory = true;
		}
	}
}
