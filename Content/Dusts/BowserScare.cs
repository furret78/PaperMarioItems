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
		private const string BowserScareLeftPath = "PaperMarioItems/Content/Dusts/BowserScare";
        private const string BowserScareRightPath = "PaperMarioItems/Content/Dusts/BowserScareRight";
        private static Asset<Texture2D> bowserScareLeft;
        private static Asset<Texture2D> bowserScareRight;
        private int selfTimer = 0;
        private int direction = 0;
        private Texture2D texture;
        private Vector2 origin;
        private Vector2 drawpos;
        private bool switchToMode2 = false; //shrink
        public override void Load()
        {
			bowserScareLeft = ModContent.Request<Texture2D>(BowserScareLeftPath);
            bowserScareRight = ModContent.Request<Texture2D>(BowserScareRightPath);
        }

        public override void OnSpawn(Dust dust) {
			if (dust.alpha != 0)
			{
				direction = dust.alpha;
				dust.alpha = 0;
			}
            dust.velocity = new(0, 0);
			dust.noGravity = true;
			dust.noLight = true;
        }

        public override bool PreDraw(Dust dust) {
            dust.frame = new Rectangle(0, 0, 128, 128);
            Color color = new(255, 255, 255);
            if (direction == 1) //player is looking rightwards
			{
				texture = (Texture2D)bowserScareRight;
                origin = new(81, 124);
                drawpos = new(dust.position.X - Main.screenPosition.X - 20, dust.position.Y - Main.screenPosition.Y);
                
            }
            else //left
            {
                texture = (Texture2D)bowserScareLeft;
                origin = new(46, 124);
                drawpos = new(dust.position.X - Main.screenPosition.X + 20, dust.position.Y - Main.screenPosition.Y);
            }
            Main.spriteBatch.Draw(texture, drawpos, dust.frame, color, dust.rotation, origin, dust.scale, SpriteEffects.None, 0f);
            return false;
        }

        public override bool Update(Dust dust) {
            //dust.alpha += 70;
            if (dust.alpha < 0) dust.alpha = 0;
            if (dust.alpha > 255) dust.alpha = 255;
            if (!switchToMode2)
            {
                dust.scale *= 1.5f;
                if (dust.scale > 3.5f)
                {
                    dust.scale = 3.5f;
                    switchToMode2 = true;
                }
            }
            else
            {
                selfTimer += 1;
                if (selfTimer >= 26)
                {
                    dust.scale -= 0.2f;
                    if (dust.scale <= 0)
                    {
                        dust.active = false;
                        switchToMode2 = false;
                        selfTimer = 0;
                    }
                }
			}
            return false;
		}
	}
}