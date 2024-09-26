using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Dusts
{
	public class StopwatchDust : ModDust
	{
        public override void OnSpawn(Dust dust) {
			dust.rotation = 0;
            dust.noGravity = true;
			dust.noLight = false;
			dust.scale = 1f;
			dust.velocity = new(0, -30);
			dust.frame = new Rectangle(0, 0, 36, 40);
        }

		public override bool Update(Dust dust) {
            dust.position += dust.velocity;
			dust.velocity *= 0.7f;
			dust.alpha += 10;
            float light = 1f * (255 - dust.alpha)/255;
			if (light > 1f) light = 1f;
            Lighting.AddLight(dust.position, light, light, light);
            if (dust.alpha >= 255) dust.active = false;
			return false;
		}
	}
}
