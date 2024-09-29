using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Treasure
{ 
	public class DubiousPaper : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 40;
			Item.value = Item.sellPrice(copper: 10);
			Item.rare = ItemRarityID.Blue;
		}
	}
}
