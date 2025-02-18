using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Walls
{
	public class CastleBleckWallTile : ModWall
	{
		public override void SetStaticDefaults() {
			Main.wallHouse[Type] = true;

			DustType = -1;

			AddMapEntry(Color.Black);
		}
	}
}