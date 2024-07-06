using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Projectiles
{
	public class CustomFireball : ModProjectile
	{
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.BallofFire);
            Projectile.width = 25;
            Projectile.height = Projectile.width;
            Projectile.penetrate = -1;
            AIType = ProjectileID.BallofFire;
        }
    }
}