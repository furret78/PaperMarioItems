using PaperMarioItems.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.ConsumablesSPM
{
	public class PowerMinus : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.PowerPlus;
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
		{
            Item.width = 39;
            Item.height = 39;
            Item.useTurn = true;
            Item.useTime = 17;
            Item.useAnimation = Item.useTime;
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = PMSoundID.useItem2;
            Item.value = Item.buyPrice(copper: 1);
        }

        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<PaperPlayer>().ResetPowerPlus(player);
            return true;
        }
	}
}
