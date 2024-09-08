using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Localization;
using PaperMarioItems.Common.Players;

namespace PaperMarioItems.Content.Items.Cooking
{ 
	public class CouplesCake : ModItem
	{
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
                player.GetModPlayer<PaperPlayer>().AddBuffCouplesCake(player);
                return true;
			}
			else return false;
        }
	}
}
