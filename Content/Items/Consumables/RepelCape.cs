using PaperMarioItems.Content.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class RepelCape : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 36;
			Item.useTurn = true;
			Item.useTime = 17;
			Item.useAnimation = Item.useTime;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = PaperMarioItems.useItemPM;
			Item.consumable = true;
			Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(copper: 25);
			Item.buffType = PMBuffID.Dodgy;
			Item.buffTime = 18000;
        }
    }
}
