using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Buffs
{
	public class ChargedBuff : ModBuff
	{
		public static readonly int DamageBonus = 15;
		public override LocalizedText Description => base.Description.WithFormatArgs(DamageBonus);
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetDamage<GenericDamageClass>() += DamageBonus / 100f;
		}
	}
}