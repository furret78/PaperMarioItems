using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Buffs
{
	public class FrightDebuff : ModBuff
	{
        public override LocalizedText Description => base.Description;
        public override void SetStaticDefaults()
        {
            Main.pvpBuff[Type] = true;
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.confused = true;
            npc.buffTime[buffIndex] = 10;
        }
    }
}