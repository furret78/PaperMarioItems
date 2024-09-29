using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Treasure
{ 
	public class PresentPaper : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 38;
			Item.height = 37;
			Item.value = Item.sellPrice(copper: 3);
			Item.rare = ItemRarityID.Pink;
		}
	}
}
