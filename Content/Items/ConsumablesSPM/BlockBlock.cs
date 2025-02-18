using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.ConsumablesSPM
{
	public class BlockBlock : ModItem
	{
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 500;
        }

        public override void SetDefaults()
		{
            Item.width = 34;
            Item.height = 38;
            Item.useTurn = true;
            Item.useTime = 17;
            Item.useAnimation = Item.useTime;
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.maxStack = Item.CommonMaxStack;
            Item.UseSound = PMSoundID.useItem2;
            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(silver: 35);
            Item.buffType = PMBuffID.BlockBlock;
            Item.buffTime = 600;
        }
	}
}
