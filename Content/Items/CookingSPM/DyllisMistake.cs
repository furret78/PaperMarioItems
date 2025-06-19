using PaperMarioItems.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.CookingSPM
{ 
	public class DyllisMistake : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.SleepySheep;
            Item.ResearchUnlockCount = 200;
        }

        public override void SetDefaults()
		{
            Item.width = 40;
            Item.height = 39;
            Item.useTurn = true;
            Item.useTime = 17;
            Item.useAnimation = Item.useTime;
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = PMSoundID.useItem;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(copper: 15);
        }

        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<PaperPlayer>().sleepySheepCooldown = Item.useTime + 1;
            player.GetModPlayer<PaperPlayer>().sleepySheepActive = true;
            return true;
        }
    }
}
