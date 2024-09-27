using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Treasure
{ 
	public class SkullGem : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 36;
			Item.height = 39;
			Item.value = Item.sellPrice(platinum: 1);
			Item.rare = ItemRarityID.Red;
		}
	}
}
