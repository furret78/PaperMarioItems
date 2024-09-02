using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Cooking
{ 
	public class PoisonMushroom : ModItem
    {
        public static LocalizedText PoisonDeath { get; private set; }
        public override void SetStaticDefaults()
        {
            PoisonDeath = this.GetLocalization(nameof(PoisonDeath));
            Item.ResearchUnlockCount = 1;
        }
        public override void SetDefaults()
		{
			Item.width = 36;
			Item.height = 39;
			Item.useTurn = true;
			Item.useTime = 17;
			Item.useAnimation = Item.useTime;
			Item.useStyle = ItemUseStyleID.EatFood;
			Item.UseSound = SoundID.Item2;
			Item.consumable = true;
			Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(silver: 20);
        }
        public override bool? UseItem(Player player)
        {
			if (Main.rand.Next(0, 15) == 1)
			{
				int healAmount = player.statLifeMax2 - player.statLife;
				player.statLife += healAmount;
				player.HealEffect(healAmount);
			}
			else
			{
				player.Hurt(
					PlayerDeathReason.ByCustomReason(player.name + " " + PoisonDeath),
					(player.statLife / 2), player.direction * (-1), false, false, -1, false, player.statDefense);
				SoundEngine.PlaySound(PaperMarioItems.causeStatusPM, player.Center);
				player.AddBuff(BuffID.Poisoned, 3600);
			}
			return true;
        }
	}
}
