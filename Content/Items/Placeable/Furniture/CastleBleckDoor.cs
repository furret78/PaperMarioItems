using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Placeable.Furniture
{
	public class CastleBleckDoor : ModItem
	{
		public override void SetDefaults() {
			Item.DefaultToPlaceableTile(PMTileID.CastleBleckDoorClosed);
			Item.width = 36;
			Item.height = 32;
			Item.value = Item.sellPrice(silver: 1, copper: 80);
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(PMItemID.CastleBleckBrick, 6)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}