using PaperMarioItems.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Buffs
{
	public class ElectrifiedBuff : ModBuff
	{
		public override LocalizedText Description => base.Description;
		public override void Update(Player player, ref int buffIndex)
		{
			player.buffImmune[BuffID.Electrified] = true;
			player.GetModPlayer<PaperPlayer>().electrifiedEffect = true;
			player.thorns += 0.5f;
		}
	}
}