using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Localization;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class PointSwap : ModItem
	{
        public static LocalizedText PointSwapDeath { get; private set; }
        public override void SetStaticDefaults()
		{
            PointSwapDeath = this.GetLocalization(nameof(PointSwapDeath));
            Item.ResearchUnlockCount = 5;
		}

        public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 40;
			Item.useTurn = true;
			Item.consumable = true;
			Item.useTime = 17;
			Item.useAnimation = Item.useTime;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(silver: 5);
        }

        public override bool? UseItem(Player player)
        {
            SoundEngine.PlaySound(PMSoundID.useItem, player.Center);
			int TempLife = player.statLife;
			if (player.statMana < player.statLifeMax2)
			{
				if (player.statMana <= 0) player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " " + PointSwapDeath), 10, 1);
				else player.statLife = player.statMana;
			}
			else player.statLife = player.statLifeMax2;
			if (TempLife < player.statManaMax2)
				player.statMana = TempLife;
			else player.statMana = player.statManaMax2;
			return true;
        }
	}
}
