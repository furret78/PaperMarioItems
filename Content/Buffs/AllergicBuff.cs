using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace PaperMarioItems.Content.Buffs
{
	public class AllergicBuff : ModBuff
	{
        public static readonly int FrameCount = 2;
        public static readonly int AnimationSpeed = 20;
        public static readonly string AnimationSheetPath = "PaperMarioItems/Content/Buffs/AllergicBuff_anim";

        private Asset<Texture2D> allergicTexture;

        public override void SetStaticDefaults()
        {
            allergicTexture = ModContent.Request<Texture2D>(AnimationSheetPath);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, int buffIndex, ref BuffDrawParams drawParams)
        {
            Texture2D buffTexture = allergicTexture.Value;
            Rectangle buffSrcRect = buffTexture.Frame(verticalFrames: FrameCount, frameY: (int)Main.GameUpdateCount / AnimationSpeed % FrameCount);
            drawParams.Texture = buffTexture;
            drawParams.SourceRectangle = buffSrcRect;
            return true;
        }
    }
}