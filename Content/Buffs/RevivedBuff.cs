using Terraria;
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

        public override LocalizedText Description => base.Description;
    }
}