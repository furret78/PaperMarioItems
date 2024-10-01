using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Weapons
{ 
	public class CoconutBomb : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemsThatCountAsBombsForDemolitionistToSpawn[Type] = true;
            ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.Coconut;
            Item.ResearchUnlockCount = 80;
        }
        public override void SetDefaults()
		{
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shootSpeed = 12f;
            Item.shoot = PMProjID.CoconutBomb;
            Item.width = 39;
            Item.height = 40;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.UseSound = SoundID.Item1;
            Item.useTime = 40;
            Item.useAnimation = Item.useTime;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.value = Item.sellPrice(silver: 5);
            Item.rare = ItemRarityID.Orange;
        }
	}
}
