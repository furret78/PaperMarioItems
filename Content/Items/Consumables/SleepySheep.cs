using PaperMarioItems.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class SleepySheep : ModItem
	{
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
            Item.value = Item.buyPrice(silver: 4);
        }

        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<PaperPlayer>().sleepySheepCooldown = Item.useTime + 1;
            player.GetModPlayer<PaperPlayer>().sleepySheepActive = true;
            return true;
        }
	}
}
