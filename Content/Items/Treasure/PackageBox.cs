using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Treasure
{ 
	public class PackageBox : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 39;
			Item.height = 33;
			Item.value = Item.sellPrice(copper: 5);
			Item.rare = ItemRarityID.White;
			Item.noUseGraphic = true;
		}
	}
}
