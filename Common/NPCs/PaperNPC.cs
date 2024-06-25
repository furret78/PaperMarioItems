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
        public static readonly Color LuckyTextColor = new Color(255, 255, 0, 255);
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
                    Dust.NewDust(npc.Center, 0, 0, DustID.Electric);
                    waitTimeDizzy++;
                }
                else
                {
                    if (waitTimeDizzy == 5) waitTimeDizzy = 0;
                    else waitTimeDizzy++;
                }
            }
        }
        public override void OnHitNPC(NPC npc, NPC target, NPC.HitInfo hit)
        {
            if (dizzyDebuff && Main.rand.NextBool(2) == false)
            {
                npc.damage = 0;
                hit.Damage = 0;
                hit.Knockback = 0;
                SuccessfulDodgeEffect(target);
            }
        }
        public override void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
        {
            if (dizzyDebuff && Main.rand.NextBool(2) == false)
            {
                npc.damage = 0;
                hurtInfo.Damage = 0;
                hurtInfo.Knockback = 0;
                target.GetModPlayer<PaperPlayer>().RepelDodge();
                /* SoundEngine.PlaySound(PaperMarioItems.luckyPM);
                Rectangle currentLocation = target.getRect();
                CombatText.NewText(currentLocation, LuckyTextColor, LuckyEvade); */
            }
        }
        public void SuccessfulDodgeEffect(NPC npc)
        {
            SoundEngine.PlaySound(PaperMarioItems.luckyPM);
            Rectangle currentLocation = npc.getRect();
            CombatText.NewText(currentLocation, LuckyTextColor, LuckyEvade);
        }
        public void DizzyDebuffImmuneTime(NPC npc)
        {

        }
    }
}