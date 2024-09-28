using PaperMarioItems.Common.Players;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Buffs
{
	public class ChargedBuff : ModBuff
	{
		private const int DamageBonus = 5;
        private int TotalExtraDamage = 0, DamageStack = 0, MaxStack = 0;

        public override LocalizedText Description => base.Description;
        public static LocalizedText StackAndDamageCount { get; private set; }

        public override void SetStaticDefaults()
        {
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
		{
			DamageStack = player.GetModPlayer<PaperPlayer>().chargedStack;
            player.GetDamage(DamageClass.Generic) += ((DamageBonus * DamageStack) / 100f);
        }

        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            Player player = Main.LocalPlayer;
            string chargeCap = "Unlimited";
            if (!NPC.downedMoonlord)
            {
                if (!Main.hardMode)
                {
                    chargeCap = player.GetModPlayer<PaperPlayer>().preHardChargeCap.ToString("0");
                    if (NPC.downedBoss3) chargeCap = player.GetModPlayer<PaperPlayer>().skellyChargeCap.ToString("0");
                }
                else
                {
                    chargeCap = player.GetModPlayer<PaperPlayer>().hardChargeCap.ToString("0");
                    if (NPC.downedMechBossAny) chargeCap = player.GetModPlayer<PaperPlayer>().mechChargeCap.ToString("0");
                    if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3) chargeCap = player.GetModPlayer<PaperPlayer>().mechAllChargeCap.ToString("0");
                    if (NPC.downedPlantBoss) chargeCap = player.GetModPlayer<PaperPlayer>().planteraChargeCap.ToString("0");
                    if (NPC.downedAncientCultist) chargeCap = player.GetModPlayer<PaperPlayer>().cultistChargeCap.ToString("0");
                }
            }
            string stackAndMax = new(DamageStack.ToString("0") + " / " + chargeCap);
            buffName = buffName.Replace("{Stack / MaxStack}", stackAndMax);
            tip = tip.Replace("{Percent}", (DamageBonus * DamageStack).ToString("0"));
        }
    }
}