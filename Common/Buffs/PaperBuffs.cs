using PaperMarioItems.Content;
using Terraria.ModLoader;

namespace PaperMarioItems.Common.Buffs
{
	public class PaperBuffs : GlobalBuff
	{
		public override bool RightClick(int type, int buffIndex) {
			if (type == PMBuffID.Revived ||
                type == PMBuffID.Charged) return false;
			return base.RightClick(type, buffIndex);
		}
	}
}