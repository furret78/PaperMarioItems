using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Treasure
{ 
	public class Superbombomb : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 39;
			Item.height = 40;
			Item.value = Item.sellPrice(silver: 60);
			Item.rare = ItemRarityID.Green;
		}
	}
}
