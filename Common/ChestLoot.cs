using PaperMarioItems.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Common
{
    public class ChestItemWorldGen : ModSystem
    {
        public override void PostWorldGen()
        {
            int itemsPlaced = 0;
            float maxItems = 80;
            float spawnRate = 10;
            if (Main.expertMode && !Main.masterMode)
            {
                maxItems *= 0.5f;
                spawnRate *= 0.5f;
            }
            if (Main.masterMode)
            {
                maxItems *= 0.2f;
                spawnRate *= 0.2f;
            }
            for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest == null)
                {
                    continue;
                }
                Tile chestTile = Main.tile[chest.x, chest.y];
                if (chestTile.TileType == TileID.Containers &&
                    (chestTile.TileFrameX == 16 * 36 ||
                    chestTile.TileFrameX == 13 * 36 ||
                    chestTile.TileFrameX == 17 * 36 ||
                    chestTile.TileFrameX == 12 * 36 ||
                    chestTile.TileFrameX == 3 * 36))
                {
                    if (!WorldGen.genRand.NextBool((int)spawnRate))
                        continue;
                    else for (int inventoryIndex = 0; inventoryIndex < Chest.maxItems; inventoryIndex++)
                    {
                        if (chest.item[inventoryIndex].type == ItemID.None)
                        {
                            chest.item[inventoryIndex].SetDefaults(PMItemID.Cookbook);
                            itemsPlaced++;
                            break;
                        }
                    }
                }
                if (itemsPlaced >= (int)maxItems)
                {
                    break;
                }
            }
        }
    }
}