using Microsoft.Xna.Framework;
using PaperMarioItems.Content;
using PaperMarioItems.Content.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace PaperMarioItems.Common.NPCs
{
    public partial class PaperNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public bool doNotRunAI, timestopOn, hadGravity, sleepOn, hadNoGravitySleep, setDirection = false;
        private int wtd = 0, tts = 0, wts = 0;
        private float oldgfxOff = 0;
        public static readonly Color LuckyTextColor = new(255, 255, 0);
        public override void ResetEffects(NPC npc)
        {
            //softDebuff = false;
            sleepOn = false;
        }
        public override bool PreAI(NPC npc)
        {
            doNotRunAI = false;
            if (npc.HasBuff(PMBuffID.Sleep))
            {
                if (!sleepOn)
                {
                    oldgfxOff = npc.gfxOffY;
                    if (npc.noGravity && !npc.noTileCollide)
                    {
                        hadNoGravitySleep = true;
                        npc.noGravity = false;
                    }
                    if (npc.direction == 1) setDirection = true;
                    else setDirection = false;
                    sleepOn = true;
                }
                /* else if (PaperNPCList.EnemiesRotateWhileAsleep.Exists(x => x == npc.type))
                {
                    npc.rotation = (float)(Math.PI / 2f * (-npc.direction));
                    npc.gfxOffY = npc.height / 2;
                } */
                doNotRunAI = true;
            }
            else
            {
                //if (PaperNPCList.EnemiesRotateWhileAsleep.Exists(x => x == npc.type)) npc.rotation = 0f;
                if (sleepOn)
                {
                    if (hadNoGravitySleep)
                    {
                        npc.velocity.Y = 0;
                        npc.noGravity = true;
                    }
                    sleepOn = false;
                }
            }
            if (npc.HasBuff(PMBuffID.Timestop))
            {
                if (!timestopOn)
                {
                    if (!npc.noGravity)
                    {
                        hadGravity = true;
                        npc.noGravity = true;
                    }
                    tts = 0;
                    timestopOn = true;
                }
                npc.frameCounter = 0;
                npc.velocity = Vector2.Zero;
                doNotRunAI = true;
            }
            else if (timestopOn)
            {
                if (hadGravity) npc.noGravity = false;
                timestopOn = false;
            }
            if (doNotRunAI) return false;
            return true;
        }

        public override void PostAI(NPC npc)
        {
            if (npc.HasBuff(PMBuffID.Sleep))
            {
                if (setDirection) npc.direction = 1;
                else npc.direction = -1;
                npc.frameCounter = 0;
                npc.velocity.X = 0;
                //TODO: make a list of enemy IDs that are exempt from gravity (ghosts, etc.)
                if (npc.velocity.Y < 0) npc.velocity.Y = 0;
                if (npc.velocity.Y < npc.maxFallSpeed) npc.velocity.Y += npc.gravity;
                if (npc.velocity.Y > npc.maxFallSpeed) npc.velocity.Y = npc.maxFallSpeed;
            }
        }

        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if (npc.HasBuff<SoftDebuff>()) modifiers.Defense *= SoftDebuff.DefenseAdjust;
        }
        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (npc.HasBuff<SoftDebuff>()) drawColor.R = 10;
            if (npc.HasBuff<DizzyDebuff>())
            {
                if (wtd == 1)
                {
                    Dust.NewDust(npc.Center, 0, 0, PMDustID.DizzyDust, Main.rand.NextFloat(-4, 5), Main.rand.NextFloat(-4, 0));
                    wtd++;
                }
                else
                {
                    if (wtd > 10) wtd = -1;
                    wtd++;
                }
            }
            if (npc.HasBuff(PMBuffID.Sleep))
            {
                if (wts == 1)
                {
                    Rectangle getLoc = npc.getRect();
                    Rectangle currentLoc = new((int)(npc.Center.X + Main.rand.NextFloat(-8, 9)), (int)(npc.Center.Y + Main.rand.NextFloat(0, 4)), getLoc.Width, getLoc.Height);
                    CombatText.NewText(currentLoc, Color.White, "Z");
                }
                else
                {
                    if (wts > 30) wts = -1;
                }
                wts++;
            }
            if (npc.HasBuff(PMBuffID.Timestop))
            {
                Color color = new(240 + Main.rand.Next(15), 240 + Main.rand.Next(15), 240 + Main.rand.Next(15));
                Vector2 newPos = new(npc.Center.X - (36/2)+4, npc.Center.Y - (40/2));
                if (tts >= 60)
                {
                    tts = 0;
                    Dust.NewDustPerfect(newPos, PMDustID.StopwatchDust, null, 0, color);
                }
                tts++;
            }
        }

        public override void ModifyHitNPC(NPC npc, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (npc.HasBuff<DizzyDebuff>())
            {
                ref StatModifier finalDamage = ref modifiers.FinalDamage;
                finalDamage *= 0f;
                ref StatModifier knockback = ref modifiers.Knockback;
                knockback *= 0f;
            }
        }

        public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
        {
            if (npc.HasBuff<DizzyDebuff>())
            {
                ref StatModifier finalDamage = ref modifiers.FinalDamage;
                finalDamage *= 0f;
                ref StatModifier knockback = ref modifiers.Knockback;
                knockback *= 0f;
            }
        }
    }
}