using PaperMarioItems.Common.Players;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.ConsumablesSPM
{
	public class BlockBlock : ModItem
	{
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
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
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(silver: 35);
        }

        public override bool? UseItem(Player player)
        {
            //insert block block functionality
            return true;
        }
	}
}
