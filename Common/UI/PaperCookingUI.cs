using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Common
{
    public class EnableCooking : GlobalTile
    {
        public override void RightClick(int i, int j, int type)
        {
            if (type == TileID.CookingPots)
            {

            }
            else base.RightClick(i, j, type);
        }
    }
}