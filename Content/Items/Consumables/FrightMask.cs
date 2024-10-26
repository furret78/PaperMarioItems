using PaperMarioItems.Common.Players;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class FrightMask : ModItem
	{
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 5;
        }

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
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(silver: 5);
        }

        public override bool? UseItem(Player player)
        {
            if (!player.GetModPlayer<PaperPlayer>().frightMaskActive && player.GetModPlayer<PaperPlayer>().frightMaskCooldown <= 0)
            {
                player.GetModPlayer<PaperPlayer>().frightMaskCooldown = Item.useTime*2;
                player.GetModPlayer<PaperPlayer>().frightMaskActive = true;
                SoundEngine.PlaySound(PMSoundID.useItem, player.Center);
                return true;
            }
            else return false;
        }
	}
}
