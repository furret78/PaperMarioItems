using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using PaperMarioItems.Common.Players;
using Terraria.Localization;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class HPDrain : ModItem
	{
        private const int healAmount = 100;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(healAmount);
        public static LocalizedText HPDrainDeath { get; private set; }
        public override void SetStaticDefaults()
		{
            HPDrainDeath = this.GetLocalization(nameof(HPDrainDeath));
            Item.ResearchUnlockCount = 5;
		}

        public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 40;
			Item.useTurn = true;
			Item.consumable = true;
			Item.useTime = 17;
			Item.useAnimation = Item.useTime;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = PaperMarioItems.useItemPM;
			Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(copper: 25);
        }

        public override bool? UseItem(Player player)
        {
			player.GetModPlayer<PaperPlayer>().HPDrain(player, healAmount, (string)HPDrainDeath);
            return true;
        }
	}
}
