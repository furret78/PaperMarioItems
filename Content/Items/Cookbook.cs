using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items
{ 
	public class Cookbook : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 30;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Orange;
		}
	}
}
