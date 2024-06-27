using Microsoft.Xna.Framework;
using PaperMarioItems.Common.Players;
using PaperMarioItems.Content.Buffs;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PaperMarioItems.Common.NPCs
{
    public class PaperNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public bool softDebuff;
        public bool dizzyDebuff;
        private int waitTimeDizzy = 0;
        public static readonly Color LuckyTextColor = new Color(255, 255, 0);
        public static readonly Color DizzyColor = new Color(0, 0, 255);
        public readonly string LuckyEvade = Language.GetTextValue($"Mods.PaperMarioItems.Common.Players.LuckyEvade");
        public override void ResetEffects(NPC npc)
        {
            softDebuff = false;
            dizzyDebuff = false;
        }
        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if (softDebuff) modifiers.Defense *= SoftDebuff.DefenseAdjust;
        }
        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (softDebuff) drawColor.G = 0;
            if (dizzyDebuff)
            {
                if (waitTimeDizzy == 0)
                {
                    Dust.NewDust(npc.Center, 0, 0, DustID.Electric, 0, 0, 0, DizzyColor);
                    waitTimeDizzy++;
                }
                else
                {
                    if (waitTimeDizzy == 5) waitTimeDizzy = 0;
                    else waitTimeDizzy++;
                }
            }
        }
        public override void ModifyHitNPC(NPC npc, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (dizzyDebuff)
            {
                ref StatModifier finalDamage = ref modifiers.FinalDamage;
                finalDamage *= 0f;
                ref StatModifier knockback = ref modifiers.Knockback;
                knockback *= 0f;
            }
        }
        public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
        {
            if (dizzyDebuff)
            {
                ref StatModifier finalDamage = ref modifiers.FinalDamage;
                finalDamage *= 0f;
                ref StatModifier knockback = ref modifiers.Knockback;
                knockback *= 0f;
            }
        }
    }
}