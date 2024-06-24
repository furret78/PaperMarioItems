using PaperMarioItems.Common.Players;
using PaperMarioItems.Content.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class DizzyDial : ModItem
	{
        public override void SetDefaults()
		{
            Item.width = 37;
            Item.height = 40;
            Item.useTurn = true;
            Item.useTime = 17;
            Item.useAnimation = Item.useTime;
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = PaperMarioItems.useItemPM;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.White;
            Item.value = Item.buyPrice(copper: 25);
            Item.buffType = ModContent.BuffType<DizzyDebuff>();
            Item.buffTime = 3600;
        }

        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<PaperPlayer>().InflictDizzyOnEnemies(player);
            return true;
        }
	}
}
