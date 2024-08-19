using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Dusts
{
	public class ElectricDust : ModDust
	{
		public override void OnSpawn(Dust dust) {
            dust.rotation = ((dust.position + dust.velocity) - dust.position).ToRotation() - MathHelper.ToRadians(90);
            dust.velocity *= 0.4f;
            dust.noGravity = true;
			dust.noLight = true;
			dust.scale *= 0.9f;
            dust.frame = new Rectangle(0, 0, 17, 48);
        }

		public override bool Update(Dust dust) {
			dust.position += dust.velocity;
			dust.scale *= 0.99f;
			float light = 1f * dust.scale;
			Lighting.AddLight(dust.position, light, light, light);
			if (dust.scale < 0.3f) dust.active = false;
			return false;
		}
	}
}
