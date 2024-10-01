using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Weapons
{ 
	public class ZessDynamite : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemsThatCountAsBombsForDemolitionistToSpawn[Type] = true;
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.CoconutBomb;
            Item.ResearchUnlockCount = 50;
        }
        public override void SetDefaults()
		{
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shootSpeed = 9f;
            Item.shoot = PMProjID.ZessDynamite;
            Item.width = 38;
            Item.height = 40;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.UseSound = SoundID.Item1;
            Item.useTime = 40;
            Item.useAnimation = Item.useTime;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.value = Item.sellPrice(silver: 20);
            Item.rare = ItemRarityID.Orange;
        }
	}
}
