using PaperMarioItems.Content;
using Terraria.ModLoader;

namespace PaperMarioItems.Common
{
    public class CrossModIntegration : ModSystem
    {
        public override void PostSetupContent()
        {
            if (ModLoader.TryGetMod("Gensokyo", out Mod gensokyoMod))
            {
                for (int i = 0; i < PMBuffID.debuffList.Count; i++)
                {
                    gensokyoMod.Call("AddTransferableDebuff", PMBuffID.debuffList[i]);
                }
            }
        }
    }
}