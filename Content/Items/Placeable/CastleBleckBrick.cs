using Terraria;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Placeable
{
	public class CastleBleckBrick : ModItem
	{
		public override void SetStaticDefaults() {
			Item.ResearchUnlockCount = 100;
		}

		public override void SetDefaults() {
			Item.DefaultToPlaceableTile(PMTileID.CastleBleckBrick);
			Item.width = 12;
			Item.height = 12;
			Item.value = Item.sellPrice(copper: 30);
		}
	}
}
