using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Dusts
{
	public class DizzyDust : ModDust
	{
        private const string DustPath = "PaperMarioItems/Content/Dusts/DizzyDust";
        private static Asset<Texture2D> dizzyDust;
        public override void Load()
        {
            dizzyDust = ModContent.Request<Texture2D>(DustPath);
        }
        public override void OnSpawn(Dust dust) {
			dust.rotation = 0;
            dust.noGravity = true;
			dust.noLight = false;
			dust.scale = 0.5f;
            dust.velocity *= 0.4f;
            dust.frame = new Rectangle(0, 0, 31, 34);
            dust.color = new(0, 255, 255, 0);
        }
        public override bool PreDraw(Dust dust)
        {
            Vector2 origin = new Vector2(dust.frame.Width / 2, dust.frame.Height / 2);
            Texture2D texture = (Texture2D)dizzyDust;
            Main.spriteBatch.Draw(texture, dust.position - Main.screenPosition, dust.frame, dust.color, dust.rotation, origin, dust.scale, SpriteEffects.None, 0f);
            return false;
        }
        public override bool Update(Dust dust) {
            dust.scale *= 0.97f;
            if (dust.scale < 0.05f) dust.active = false;
            dust.position += dust.velocity;
            dust.rotation += 50;
            return false;
		}
	}
}
