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

			AddMapEntry(Color.Black);
		}

		public override void NumDust(int i, int j, bool fail, ref int num) {
			num = fail ? 1 : 3;
		}
	}
}