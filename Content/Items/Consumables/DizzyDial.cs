using PaperMarioItems.Common.Players;
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
        }

        public override bool? UseItem(Player player)
        {
            if (!player.GetModPlayer<PaperPlayer>().inflictDizzyActive)
            {
                player.GetModPlayer<PaperPlayer>().InflictDizzy(player);
                return true;
            }
            //player.GetModPlayer<PaperPlayer>().InflictDizzyOnEnemies(player);
            else return false;
        }
	}
}
