using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Buffs
{
	public class PartnerBuff : ModBuff
	{
        public override LocalizedText Description => base.Description;

        public override void SetStaticDefaults()
        {
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
        }

        //the calculated stats here are for show
        //see UltraStone.cs for the actual effect
        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            int shineNum = 0;
            Player player = Main.LocalPlayer;

            if (player.HasItem(PMItemID.ShineSprite))
            {
                shineNum = player.CountItem(PMItemID.ShineSprite, 9999);
            }
            tip = tip.Replace("{Damage1}", (shineNum * (3 / 100f)).ToString("0.00"));
            tip = tip.Replace("{Damage2}", shineNum.ToString("0"));
            tip = tip.Replace("{Knockback}", (shineNum * (9 / 100f)).ToString("0.00"));
        }
    }
}