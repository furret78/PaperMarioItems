using Microsoft.Xna.Framework;
using PaperMarioItems.Content;
using PaperMarioItems.Content.Buffs;
using PaperMarioItems.Content.Dusts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Common.NPCs
{
    public partial class PaperNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public bool timestopDebuff, timestopOn, hadGravity = false;
        private int waitTimeDizzy = 0, timerTimestop = 0;
        public static readonly Color LuckyTextColor = new(255, 255, 0);
        public override void ResetEffects(NPC npc)
        {
            //softDebuff = false;
            timestopDebuff = false;
        }
        public override bool PreAI(NPC npc)
        {
            if (!timestopDebuff && timestopOn)
            {
                timestopOn = false;
                if (hadGravity) npc.noGravity = false;
            }
            if (timestopDebuff)
            {
                Color color = new(240 + Main.rand.Next(15), 240 + Main.rand.Next(15), 240 + Main.rand.Next(15));
                Vector2 newPos = new(npc.Center.X, npc.Center.Y);
                if (timerTimestop >= 60)
                {
                    timerTimestop = 0;
                    Dust.NewDustPerfect(newPos, PMDustID.StopwatchDust, null, 0, color);
                }
                timerTimestop++;
                if (!timestopOn)
                {
                    if (!npc.noGravity)
                    {
                        hadGravity = true;
                        npc.noGravity = true;
                    }
                    timerTimestop = 0;
                    timestopOn = true;
                }
                npc.frameCounter = 0;
                npc.velocity = Vector2.Zero;
                return false;
            }
            else return true;
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
                if (waitTimeDizzy == 1)
                {
                    Dust.NewDust(npc.Center, 0, 0, PMDustID.DizzyDust, Main.rand.NextFloat(-4, 5), Main.rand.NextFloat(-4, 0));
                    waitTimeDizzy++;
                }
                else
                {
                    if (waitTimeDizzy > 10) waitTimeDizzy = 0;
                    waitTimeDizzy++;
                }
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