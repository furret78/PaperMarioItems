using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Treasure
{ 
	public class VitalPaper : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 38;
			Item.height = 37;
			Item.maxStack = Item.CommonMaxStack;
			Item.value = Item.sellPrice(copper: 5);
			Item.rare = ItemRarityID.White;
		}
	}
}
