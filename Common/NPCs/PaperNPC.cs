using Microsoft.Xna.Framework;
using PaperMarioItems.Content.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace PaperMarioItems.Common.NPCs
{
    public class PaperNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public bool softDebuff;
        public override void ResetEffects(NPC npc)
        {
            softDebuff = false;
        }
        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if (softDebuff) modifiers.Defense *= SoftDebuff.DefenseAdjust;
        }
        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (softDebuff) drawColor.G = 0;
        }
    }
}