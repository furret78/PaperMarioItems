using PaperMarioItems.Common.Players;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class FrightMask : ModItem
	{
        public override void SetDefaults()
		{
            Item.width = 38;
            Item.height = 40;
            Item.useTurn = true;
            Item.useTime = 17;
            Item.useAnimation = Item.useTime;
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.White;
            Item.value = Item.buyPrice(copper: 25);
        }

        public override bool? UseItem(Player player)
        {
            if (!player.GetModPlayer<PaperPlayer>().frightMaskActive && player.GetModPlayer<PaperPlayer>().frightMaskCooldown <= 0)
            {
                player.GetModPlayer<PaperPlayer>().frightMaskCooldown = Item.useTime*2;
                player.GetModPlayer<PaperPlayer>().frightMaskActive = true;
                SoundEngine.PlaySound(PaperMarioItems.useItemPM);
                return true;
            }
            else return false;
        }
	}
}
