using PaperMarioItems.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Common.UI
{
    public class EnableCooking : GlobalTile
    {
        public override void RightClick(int i, int j, int type)
        {
            if (type == TileID.CookingPots && Main.LocalPlayer.HasItemInInventoryOrOpenVoidBag(PMItemID.Cookbook))
            {
                ModContent.GetInstance<PaperCookingSystem>().NearestCookingPotPosition = ModContent.GetInstance<PaperCookingSystem>().NearestCookingPotPosition == null ? new Point16(i, j).ToWorldCoordinates() : null;
                return;
            }
        }

        public override void MouseOver(int i, int j, int type)
        {
            if (type == TileID.CookingPots && Main.LocalPlayer.HasItemInInventoryOrOpenVoidBag(PMItemID.Cookbook))
            {
                Player player = Main.LocalPlayer;
                player.cursorItemIconID = ItemID.CookingPot;
                player.noThrow = 2;
                player.cursorItemIconEnabled = true;
            }
        }

        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (type == TileID.CookingPots && ModContent.GetInstance<PaperCookingSystem>().NearestCookingPotPosition != null)
                ModContent.GetInstance<PaperCookingSystem>().NearestCookingPotPosition = null;
        }

        public override void Unload()
        {
            if (ModContent.GetInstance<PaperCookingSystem>().NearestCookingPotPosition != null)
                ModContent.GetInstance<PaperCookingSystem>().NearestCookingPotPosition = null;
        }
    }
}