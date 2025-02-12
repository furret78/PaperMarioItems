using PaperMarioItems.Common.Players;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Buffs
{
    /// <summary>
    /// ChargedBuff is only for show. Use <see cref="PaperPlayer.DrinkHotSauce(Player)"/> to apply the status.
    /// </summary>
	public class ChargedBuff : ModBuff
	{
		private const int DamageBonus = 5;
        private int DamageStack = 0;

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
            var modPlayer = Main.LocalPlayer.GetModPlayer<PaperPlayer>();
            string chargeCap = "Unlimited";
            if (!NPC.downedMoonlord)
            {
                if (!Main.hardMode)
                {
                    chargeCap = modPlayer.preHardChargeCap.ToString("0");
                    if (NPC.downedBoss3) chargeCap = modPlayer.skellyChargeCap.ToString("0");
                }
                else
                {
                    chargeCap = modPlayer.hardChargeCap.ToString("0");
                    if (NPC.downedMechBossAny) chargeCap = modPlayer.mechChargeCap.ToString("0");
                    if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3) chargeCap = modPlayer.mechAllChargeCap.ToString("0");
                    if (NPC.downedPlantBoss) chargeCap = modPlayer.planteraChargeCap.ToString("0");
                    if (NPC.downedAncientCultist) chargeCap = modPlayer.cultistChargeCap.ToString("0");
                }
            }
            string stackAndMax = new(DamageStack.ToString("0") + " / " + chargeCap);
            buffName = buffName.Replace("{Stack / MaxStack}", stackAndMax);
            tip = tip.Replace("{Percent}", (DamageBonus * DamageStack).ToString("0"));
        }
    }
}