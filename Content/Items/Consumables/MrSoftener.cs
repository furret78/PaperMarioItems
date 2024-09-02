using PaperMarioItems.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{
	public class MrSoftener : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 40;
			Item.useTurn = true;
			Item.useTime = 17;
			Item.useAnimation = Item.useTime;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.consumable = true;
			Item.maxStack = Item.CommonMaxStack;
			Item.rare = ItemRarityID.Orange;
			Item.value = Item.buyPrice(silver: 6);
		}

        public override bool? UseItem(Player player)
        {
			if (!player.GetModPlayer<PaperPlayer>().causeSoften)
			{
				player.GetModPlayer<PaperPlayer>().causeSoften = true;
                return true;
			}
			return false;
        }
    }
}
