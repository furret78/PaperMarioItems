using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items
{ 
	public class ZessTPot : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 30;
			Item.value = Item.buyPrice(silver: 1);
			Item.rare = ItemRarityID.Orange;
		}
	}
}
