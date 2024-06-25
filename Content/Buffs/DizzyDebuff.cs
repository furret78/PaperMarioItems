using PaperMarioItems.Common.NPCs;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Buffs
{
	public class DizzyDebuff : ModBuff
	{
        public override LocalizedText Description => base.Description;
        public override void SetStaticDefaults()
        {
            Main.pvpBuff[Type] = true;
            Main.debuff[Type] = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<PaperNPC>().dizzyDebuff = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.confused = true;
        }
    }
}