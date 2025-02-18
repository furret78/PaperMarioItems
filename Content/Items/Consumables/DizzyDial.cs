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
            Item.UseSound = PMSoundID.useItem;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(silver: 8);
        }

        public override bool? UseItem(Player player)
        {
            /*if (!player.GetModPlayer<PaperPlayer>().inflictDizzyActive)
            {
                player.GetModPlayer<PaperPlayer>().InflictDizzy();
                return true;
            }*/
            player.GetModPlayer<PaperPlayer>().InflictDizzyOnEnemies(player);
            return true;
        }
	}
}
