using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Dusts
{
	public class LightningDust : ModDust
	{
		private const string LightningDustPath = "PaperMarioItems/Content/Dusts/LightningDust";
		private static Asset<Texture2D> lightningDust;

        public override void Load()
        {
			lightningDust = ModContent.Request<Texture2D>(LightningDustPath);
        }

        public override void OnSpawn(Dust dust) {
            dust.rotation = 0;
            dust.velocity = new Vector2(0, 0);
			dust.noGravity = true;
			dust.scale = 0.1f;
			dust.frame = new Rectangle(0, 0, 171, 179);
			dust.noLight = false;
        }

        public override bool PreDraw(Dust dust)
        {
			Vector2 origin = new Vector2(dust.frame.Width/2,dust.frame.Height/2);
			Texture2D texture = (Texture2D)lightningDust;
			Color color = new Color(255, 255, 0, 0);
			Main.spriteBatch.Draw(texture, dust.position - Main.screenPosition, dust.frame, color, dust.rotation, origin, dust.scale, SpriteEffects.None, 0f);
            return false;
        }

        public override bool Update(Dust dust) {
			dust.rotation += 20f;
			dust.scale *= 1.2f;

			if (dust.scale > 1f) dust.alpha += 10;
			if (dust.alpha >= 255) dust.active = false;
			return false;
		}
	}
}
