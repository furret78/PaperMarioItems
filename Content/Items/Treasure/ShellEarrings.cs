using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Treasure
{ 
	public class ShellEarrings : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 39;
			Item.height = 38;
			Item.value = Item.sellPrice(silver: 50);
			Item.rare = ItemRarityID.Pink;
			Item.accessory = true;
		}
	}
}
