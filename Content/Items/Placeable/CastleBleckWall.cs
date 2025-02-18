using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Placeable
{
	public class CastleBleckWall : ModItem
	{
		public override void SetStaticDefaults() {
			Item.ResearchUnlockCount = 400;
		}

		public override void SetDefaults() {
			Item.DefaultToPlaceableWall(PMWallID.CastleBleckWall);
			Item.width = 24;
			Item.height = Item.width;
			Item.value = Item.sellPrice(copper: 1);
		}

		public override void AddRecipes() {
			CreateRecipe(4)
				.AddIngredient(PMItemID.CastleBleckBrick)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}
