using PaperMarioItems.Common.Players;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Buffs
{
	public class BlockBlockBuff : ModBuff
	{
        public override LocalizedText Description => base.Description;

		public override void Update(Player player, ref int buffIndex) {
			player.GetModPlayer<PaperPlayer>().blockBlockEffect = true;
			player.SetImmuneTimeForAllTypes(2);
			player.immuneNoBlink = true;
		}
	}
}