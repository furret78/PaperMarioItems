using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class RepelCape : ModItem
	{
        private int effectTime = 5;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(effectTime);
        public override void SetStaticDefaults()
        {
			ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.BoosSheet;
        }
        public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 36;
			Item.useTurn = true;
			Item.useTime = 17;
			Item.useAnimation = Item.useTime;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = PMSoundID.useItem;
			Item.consumable = true;
			Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(silver: 15);
			Item.buffType = PMBuffID.Dodgy;
			Item.buffTime = effectTime*60*60;
        }
    }
}
