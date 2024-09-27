using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Treasure
{ 
	public class MoonStone : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 35;
			Item.height = 35;
			Item.value = Item.sellPrice(gold: 5);
			Item.rare = ItemRarityID.Yellow;
		}
	}
}
