using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Cooking
{ 
	public class TrialStew : ModItem
    {
        public override void SetDefaults()
		{
			Item.width = 36;
			Item.height = 39;
			Item.useTurn = true;
			Item.useTime = 17;
			Item.useAnimation = Item.useTime;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.consumable = true;
			Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Gray;
            Item.value = Item.sellPrice(silver: 1);
        }
        public override bool? UseItem(Player player)
        {
            if (player.statLife == 0 && player.statMana == 0) return false;
            else
            {
                SoundEngine.PlaySound(PaperMarioItems.useItemPM, player.Center);
                player.statLife = 1;
                player.statMana = (player.statManaMax2/2)*(-1);
                return true;
            }
        }
	}
}
