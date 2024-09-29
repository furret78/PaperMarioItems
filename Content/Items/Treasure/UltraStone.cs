using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Treasure
{ 
	public class UltraStone : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 34;
			Item.height = 36;
			Item.value = Item.sellPrice(gold: 17);
			Item.rare = ItemRarityID.Red;
		}
	}
}
