using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Dusts
{
	public class BowserScare : ModDust
	{
		private const string BowserScarePath = "PaperMarioItems/Content/Dusts/BowserScare";
		private static Asset<Texture2D> bowserScare;
		private int selfTimer = 180;
        public override void Load()
        {
			bowserScare = ModContent.Request<Texture2D>(BowserScarePath);
        }

        public override void OnSpawn(Dust dust) {
            dust.rotation = 90;
            dust.velocity = new Vector2(0, 0);
			dust.noGravity = true;
			dust.scale = 0.1f;
			dust.alpha = 255;
			dust.frame = new Rectangle(0, 0, 128, 128);
			dust.noLight = false;
        }

        public override bool PreDraw(Dust dust)
        {
			Vector2 origin = new Vector2(46, 124);
			Texture2D texture = (Texture2D)bowserScare;
			Color color = new Color(255, 255, 0, dust.alpha);
			Main.spriteBatch.Draw(texture, dust.position - Main.screenPosition, dust.frame, color, dust.rotation, origin, dust.scale, SpriteEffects.None, 0f);
            return false;
        }

        public override bool Update(Dust dust) {
			//show up
			if (dust.alpha > 0 && selfTimer > 60) dust.alpha -= 50;
			//vanish
			else if (dust.alpha < 255 && selfTimer <= 60) dust.alpha += 60;
			if (dust.alpha < 0) dust.alpha = 0;
			if (dust.alpha > 255) dust.alpha = 255;
			//rotate back to upright
			if (dust.rotation < 0) dust.rotation += 10;
			if (dust.rotation > 0) dust.rotation -= 10;
			//expand
			if (selfTimer > 60 && dust.scale < 3f) dust.scale *= 1.3f;
			if (selfTimer <= 60) dust.scale += 0.5f;
			if (selfTimer <= 0 && dust.alpha >= 255) dust.active = false;
			selfTimer++;
			return false;
		}
	}
}
