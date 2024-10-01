using PaperMarioItems.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class POWBlock : ModItem
	{
        public const int powDamage = 20;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(powDamage);

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 20;
        }

        public override void SetDefaults()
		{
            Item.width = 40;
            Item.height = 40;
            Item.useTurn = true;
            Item.useTime = 20;
            Item.useAnimation = Item.useTime;
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = PMSoundID.useItem;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.buyPrice(silver: 5);
        }

        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<PaperPlayer>().PowBlock(player, powDamage);
            return true;
        }
	}
}
