using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace PaperMarioItems.Common
{
    public class ScreenShakeSystem : ModSystem
    {
        public static int screenShakeTime = 0;
        public const float screenShakeStrength = 16;
        public override void ModifyScreenPosition()
        {
            if (screenShakeTime-- > 0)
                Main.screenPosition += new Vector2(
                    Main.rand.NextFloat(-screenShakeStrength / 2, screenShakeStrength / 2),
                    Main.rand.NextFloat(-screenShakeStrength / 2, screenShakeStrength / 2)
                );
        }
    }
}