using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Buffs
{
	public class HugeBuff : ModBuff
	{
		public static readonly int DamageBonus = 10;
		public override LocalizedText Description => base.Description.WithFormatArgs(DamageBonus);
		public override void Update(Player player, ref int buffIndex) {
			player.GetDamage<GenericDamageClass>() += DamageBonus / 100f;
		}
	}
}