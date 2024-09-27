using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Buffs
{
	public class PaybackBuff : ModBuff
	{
        public override LocalizedText Description => base.Description;
		public override void Update(Player player, ref int buffIndex) {
			player.thorns += 2f;
		}
	}
}