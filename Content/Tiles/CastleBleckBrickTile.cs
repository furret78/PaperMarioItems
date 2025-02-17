using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Tiles
{
	public class CastleBleckBrickTile : ModTile
	{
		public override void SetStaticDefaults() {
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileBrick[Type] = true;

			DustType = -1;

			AddMapEntry(Color.Black);
		}
	}
}