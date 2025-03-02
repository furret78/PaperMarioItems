using Microsoft.Xna.Framework;
using PaperMarioItems.Content;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Common
{
    public class ShakeTrees : GlobalTile
    {
        public override bool ShakeTree(int x, int y, TreeTypes treeType)
        {
            Vector2 position = new(x * 16, y * 16), defaultRandomBox = new(16, 16);
            double skyHeight = Main.worldSurface * (13/20);
            bool isSpace = (float)(y / skyHeight) >= 1f && y < Main.worldSurface;

            //Ash trees (Hell) drop Primordial Fruit
            if (treeType == TreeTypes.Ash && WorldGen.genRand.NextBool(15))
            {
                Item.NewItem(source: WorldGen.GetItemSource_FromTreeShake(x, y), pos: position, randomBox: defaultRandomBox,
                    Type: PMItemID.PrimordialFruit);
                return true;
            }

            //Jungle trees drop Mild Cocoa Beans
            if (treeType == TreeTypes.Jungle && WorldGen.genRand.NextBool(8))
            {
                Item.NewItem(source: WorldGen.GetItemSource_FromTreeShake(x, y), pos: position, randomBox: defaultRandomBox,
                    Type: PMItemID.MildCocoaBean);
                return true;
            }

            //Mushroom trees drop Slimy Mushroom
            if (treeType == TreeTypes.Mushroom && WorldGen.genRand.NextBool(10))
            {
                Item.NewItem(source: WorldGen.GetItemSource_FromTreeShake(x, y), pos: position, randomBox: defaultRandomBox,
                    Type: PMItemID.SlimyMushroom);
                return true;
            }

            //Floating Island trees drop one of the four Apples
            if (((treeType == TreeTypes.Forest && isSpace) || treeType == TreeTypes.Hallowed) &&
                ((WorldGen.genRand.NextBool(3) && Main.hardMode) || (WorldGen.genRand.NextBool(15) && !Main.hardMode)))
            {
                int type;
                switch (WorldGen.genRand.Next(4))
                {
                    case 0:
                        type = PMItemID.BlackApple; break;
                    case 1:
                        type = PMItemID.BlueApple; break;
                    case 2:
                        type = PMItemID.OrangeApple; break;
                    case 3:
                        type = PMItemID.PinkApple; break;
                    default:
                        type = ItemID.Apple; break;
                }
                Item.NewItem(source: WorldGen.GetItemSource_FromTreeShake(x, y), pos: position, randomBox: defaultRandomBox,
                    Type: type);
                return true;
            }
            return false;
        }
    }
}