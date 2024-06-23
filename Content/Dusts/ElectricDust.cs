using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Dusts
{
	public class ElectricDust : ModDust
	{
		public override void OnSpawn(Dust dust) {
            dust.rotation = ((dust.position + dust.velocity) - dust.position).ToRotation() - MathHelper.ToRadians(90); //set rotation
            dust.velocity *= 0.4f; // Multiply the dust's start velocity by 0.4, slowing it down
            dust.noGravity = true; // Makes the dust have no gravity.
			dust.noLight = true; // Makes the dust emit no light.
			dust.scale *= 0.9f; // Multiplies the dust's initial scale by 1.5.
            dust.frame = new Rectangle(0, 0, 17, 48);
        }

		public override bool Update(Dust dust) { // Calls every frame the dust is active
			dust.position += dust.velocity;
			dust.scale *= 0.99f;

			float light = 1f * dust.scale;

			Lighting.AddLight(dust.position, light, light, light);

			if (dust.scale < 0.3f) {
				dust.active = false;
			}

			return false; // Return false to prevent vanilla behavior.
		}
	}
}
