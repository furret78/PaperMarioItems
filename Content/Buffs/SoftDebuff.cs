using PaperMarioItems.Common.NPCs;
using PaperMarioItems.Common.Players;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Buffs
{
	public class SoftDebuff : ModBuff
	{
		public const int ReduceDefBy = 20;
		public static float DefenseAdjust = 1 - ReduceDefBy / 100f;
        public override LocalizedText Description => base.Description.WithFormatArgs(ReduceDefBy);
        public override void SetStaticDefaults()
        {
            Main.pvpBuff[Type] = true;
            Main.debuff[Type] = true;
        }
        /*
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<PaperNPC>().softDebuff = true;
        }
        */
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<PaperPlayer>().softEffect = true;
            player.statDefense *= DefenseAdjust;
        }
    }
}