using PaperMarioItems.Content.Items;
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
            int maxItems = 5;
            int spawnRate = 20;
            if (Main.expertMode) spawnRate = 15;
            if (Main.masterMode) spawnRate = 10;
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
                    chestTile.TileFrameX == 12 * 36))
                {
                    if (WorldGen.genRand.NextBool(spawnRate))
                        continue;
                    for (int inventoryIndex = 0; inventoryIndex < Chest.maxItems; inventoryIndex++)
                    {
                        if (chest.item[inventoryIndex].type == ItemID.None)
                        {
                            chest.item[inventoryIndex].SetDefaults(ModContent.ItemType<Cookbook>());
                            itemsPlaced++;
                            break;
                        }
                    }
                }
                if (itemsPlaced >= maxItems)
                {
                    break;
                }
            }
        }
    }
}