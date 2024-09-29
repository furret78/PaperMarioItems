using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Treasure
{ 
	public class VintageRed : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 40;
			Item.consumable = true;
			Item.useTime = 17;
			Item.useAnimation = Item.useTime;
			Item.useStyle = ItemUseStyleID.DrinkLiquid;
			Item.UseSound = SoundID.Item3;
			Item.value = Item.sellPrice(silver: 20);
			Item.rare = ItemRarityID.LightRed;
			Item.buffType = BuffID.Tipsy;
			Item.buffTime = 36000;
		}
	}
}