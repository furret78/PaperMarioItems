using PaperMarioItems.Common.UI;
using PaperMarioItems.Content.Items;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Common
{
    public class EnableCooking : GlobalTile
    {
        public override void RightClick(int i, int j, int type)
        {
            base.RightClick(i, j, type);
            if (type == TileID.CookingPots && Main.LocalPlayer.HasItemInInventoryOrOpenVoidBag(ModContent.ItemType<Cookbook>()))
            {
                ModContent.GetInstance<PaperCookingSystem>().NearestCookingPotPosition = ModContent.GetInstance<PaperCookingSystem>().NearestCookingPotPosition == null ? new Point16(i, j).ToWorldCoordinates() : null;
                return;
            }
        }

        public override void MouseOver(int i, int j, int type)
        {
            base.MouseOver(i, j, type);
            if (type == TileID.CookingPots && Main.LocalPlayer.HasItemInInventoryOrOpenVoidBag(ModContent.ItemType<Cookbook>()))
            {

                Player player = Main.LocalPlayer;
                player.cursorItemIconID = ItemID.CookingPot;

                player.noThrow = 2;
                player.cursorItemIconEnabled = true;
            }
        }

        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            base.KillTile(i, j, type, ref fail, ref effectOnly, ref noItem);
            if (ModContent.GetInstance<PaperCookingSystem>().NearestCookingPotPosition != null) ModContent.GetInstance<PaperCookingSystem>().NearestCookingPotPosition = null;
        }
    }
}