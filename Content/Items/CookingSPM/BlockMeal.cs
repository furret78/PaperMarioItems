using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.CookingSPM
{
	public class BlockMeal : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.BlockBlock;
            Item.ResearchUnlockCount = 500;
        }

        public override void SetDefaults()
		{
            Item.width = 40;
            Item.height = 40;
            Item.useTurn = true;
            Item.useTime = 17;
            Item.useAnimation = Item.useTime;
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.maxStack = Item.CommonMaxStack;
            Item.UseSound = PMSoundID.useItem2;
            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(silver: 53);
            Item.buffType = PMBuffID.BlockBlock;
            Item.buffTime = 900;
        }
	}
}
