using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Treasure
{
    [AutoloadEquip(EquipType.Body)]
    public class ChampsBelt : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 40;
			Item.rare = ItemRarityID.Expert;
			Item.value = Item.sellPrice(platinum: 5);
			Item.accessory = true;
		}
    }
}
