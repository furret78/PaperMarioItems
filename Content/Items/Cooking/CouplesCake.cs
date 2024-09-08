using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using PaperMarioItems.Common.Players;

namespace PaperMarioItems.Content.Items.Cooking
{
	public class CouplesCake : ModItem
	{
        private int effectTime = 15;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(effectTime);
        public override void SetDefaults()
		{
			Item.width = 39;
			Item.height = 39;
			Item.useTurn = true;
			Item.consumable = true;
			Item.useTime = 17;
			Item.useAnimation = Item.useTime;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(silver: 30);
        }

        public override bool? UseItem(Player player)
        {
			if (player.GetModPlayer<PaperPlayer>().SearchTeammate(player))
			{
                SoundEngine.PlaySound(PaperMarioItems.useItemPM, player.Center);
                player.GetModPlayer<PaperPlayer>().AddBuffCouplesCake(player, effectTime*60*60);
                return true;
			}
			else return false;
        }
	}
}
