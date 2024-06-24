using Microsoft.Xna.Framework;
using Mono.Cecil;
using PaperMarioItems.Common.Players;
using PaperMarioItems.Content.Buffs;
using System.Drawing.Imaging;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace PaperMarioItems.Common.NPCs
{
    public partial class PaperNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public bool softDebuff;
        public bool dizzyDebuff;
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
        }
        public override void OnHitNPC(NPC npc, NPC target, NPC.HitInfo hit)
        {
            if (dizzyDebuff && Main.rand.NextBool(2) == false)
            {
                npc.justHit = false;
                hit.Damage = 0;
                hit.Knockback = 0;
                SuccessfulDodgeEffect(target);
            }
        }
        public override void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
        {
            if (dizzyDebuff && Main.rand.NextBool(2) == false)
            {
                npc.justHit = false;
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