using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Treasure
{ 
	public class GoldbobsPass : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 38;
			Item.height = 37;
			Item.value = Item.sellPrice(copper: 5);
			Item.rare = ItemRarityID.Yellow;
		}
	}
}
