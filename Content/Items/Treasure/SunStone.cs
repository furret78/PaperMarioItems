using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Treasure
{ 
	public class SunStone : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 39;
			Item.value = Item.sellPrice(gold: 5);
			Item.rare = ItemRarityID.Yellow;
		}
	}
}
