using Terraria;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Buffs
{
	public class PowerUpBuff : ModBuff
	{
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) *= 2;
        }
    }
}