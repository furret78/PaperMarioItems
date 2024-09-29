using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Treasure
{ 
	public class DataDisk : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 33;
			Item.height = 34;
			Item.value = Item.sellPrice(silver: 5);
			Item.rare = ItemRarityID.Orange;
		}
	}
}
