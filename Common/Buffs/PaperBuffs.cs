using PaperMarioItems.Content.Buffs;
using Terraria.ModLoader;

namespace PaperMarioItems.Common.Buffs
{
	public class PaperBuffs : GlobalBuff
	{
		public override bool RightClick(int type, int buffIndex) {
			if (type == ModContent.BuffType<DizzyDebuff>() ||
				type == ModContent.BuffType<SoftDebuff>() ||
				type == ModContent.BuffType<RevivedBuff>()) return false;

			return base.RightClick(type, buffIndex);
		}
	}
}