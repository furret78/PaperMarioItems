using PaperMarioItems.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Buffs
{
	public class RevivedBuff : ModBuff
	{
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<PaperPlayer>().lifeShroomRevive = true;
            if (player.HasBuff(BuffID.OnFire)) player.ClearBuff(BuffID.OnFire);
            if (player.HasBuff(BuffID.OnFire3)) player.ClearBuff(BuffID.OnFire3);
        }

        public override LocalizedText Description => base.Description;
    }
}