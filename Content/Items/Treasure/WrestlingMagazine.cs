using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Treasure
{ 
	public class WrestlingMagazine : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 37;
			Item.height = 38;
			Item.value = Item.sellPrice(copper: 50);
			Item.rare = ItemRarityID.Blue;
		}
	}
}
