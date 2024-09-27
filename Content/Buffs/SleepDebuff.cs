using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Buffs
{
	public class SleepDebuff : ModBuff
	{
        public override LocalizedText Description => base.Description;
        public override void SetStaticDefaults()
        {
            Main.pvpBuff[Type] = true;
            Main.debuff[Type] = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            //npc.GetGlobalNPC<PaperNPC>().sleepDebuff = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            if (!player.sleeping.isSleeping) player.sleeping.StartSleeping(player, (int)player.position.X, (int)(player.position.Y + player.height));
            player.moveSpeed = 0f;
            player.dash = 0;
            player.dashType = 0;
            player.noKnockback = true;
            //no more inputs
            player.controlJump = false;
            player.controlLeft = false;
            player.controlRight = false;
            player.controlDown = false;
            player.controlUp = false;
            player.controlHook = false;
            player.controlQuickHeal = false;
            player.controlQuickMana = false;
            player.controlThrow = false;
            player.controlUseItem = false;
            player.controlUseTile = false;
            player.controlDownHold = false;
        }
    }
}