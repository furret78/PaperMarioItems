using Microsoft.Xna.Framework;
using PaperMarioItems.Common.NPCs;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Buffs
{
	public class TimestopDebuff : ModBuff
	{
        public override LocalizedText Description => base.Description;
        public override void SetStaticDefaults()
        {
            Main.pvpBuff[Type] = true;
            Main.debuff[Type] = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<PaperNPC>().timestopDebuff = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            if (player.velocity.Y != 0f)
            {
                player.velocity = new Vector2(0f, 1E-06f);
            }
            else
            {
                player.velocity = Vector2.Zero;
            }
            player.gravity = 0f;
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