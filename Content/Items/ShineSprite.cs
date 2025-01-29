using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items
{ 
	public class ShineSprite : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 30;
			Item.value = Item.buyPrice(gold: 1);
			Item.maxStack = Item.CommonMaxStack;
			Item.rare = ItemRarityID.Yellow;
		}

		public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.LightGoldenrodYellow.ToVector3() * 0.77f * Main.essScale);
        }
	}
}
