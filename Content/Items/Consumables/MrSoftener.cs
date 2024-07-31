using PaperMarioItems.Common.Players;
using PaperMarioItems.Content.Buffs;
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
			Item.value = Item.buyPrice(copper: 25);
			Item.buffType = ModContent.BuffType<PaybackBuff>();
			Item.buffTime = 7200;
		}

        public override bool? UseItem(Player player)
        {
			if (!player.GetModPlayer<PaperPlayer>().ruinPowderActive && player.GetModPlayer<PaperPlayer>().ruinPowderCooldown <= 0)
			{
				player.GetModPlayer<PaperPlayer>().ruinPowderActive = true;
				player.GetModPlayer<PaperPlayer>().ruinPowderCooldown = Item.useTime * 2 + 1;
                return true;
			}
			return false;
        }
	}
}
