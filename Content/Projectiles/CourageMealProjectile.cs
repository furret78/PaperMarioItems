using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Projectiles
{
    public class CourageMealProjectile : ModProjectile
    {
        private const int DefaultHeight = 15;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.PlayerHurtDamageIgnoresDifficultyScaling[Type] = false;
        }

        public override void SetDefaults()
        {
            Projectile.width = DefaultHeight - 2;
            Projectile.height = DefaultHeight;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            DrawOffsetX = -(int)(Projectile.width / 2f);
            DrawOriginOffsetY = -(int)(Projectile.height / 2f);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.soundDelay == 0)
            {
                SoundEngine.PlaySound(SoundID.Run);
            }
            Projectile.soundDelay = 10;

            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = oldVelocity.X * -0.4f;
            }
            if (Projectile.velocity.Y != oldVelocity.Y && oldVelocity.Y > 0.7)
            {
                Projectile.velocity.Y = oldVelocity.Y * -0.4f;
            }
            return false;
        }

        public override void AI()
        {
            if (Projectile.timeLeft < 16)
            {
                Projectile.alpha += (int)(255 / 15f);
                if (Projectile.timeLeft <= 0) Projectile.Kill();
            }

            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] > 10f)
            {
                Projectile.ai[0] = 10f;
                if (Projectile.velocity.Y == 0f && Projectile.velocity.X != 0f)
                {
                    Projectile.velocity.X = Projectile.velocity.X * 0.96f;
                    if (Projectile.velocity.X > -0.01 && Projectile.velocity.X < 0.01)
                    {
                        Projectile.velocity.X = 0f;
                        Projectile.netUpdate = true;
                    }
                }
                Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;
            }
            Projectile.rotation += Projectile.velocity.X * 0.1f;

            if (Projectile.velocity.X > 0.1f || Projectile.velocity.Y > 0.1f)
            {
                foreach (var npc in Main.ActiveNPCs)
                {
                    if (npc.friendly) continue;
                    if (Projectile.Colliding(Projectile.getRect(), npc.getRect())) CourageMealDelete();
                }
                foreach (var vsplayer in Main.ActivePlayers)
                {
                    if (!vsplayer.hostile) continue;
                    if (Projectile.Colliding(Projectile.getRect(), vsplayer.getRect())) CourageMealDelete();
                }
            }
        }

        private void CourageMealDelete()
        {
            Projectile.ai[1] = 1;
            Projectile.damage = 100;
            Projectile.knockBack = 5f;
        }

        public override void OnKill(int timeLeft)
        {
            if (Projectile.ai[1] == 1) SoundEngine.PlaySound(PMSoundID.damage, Projectile.Center);
        }
    }
}