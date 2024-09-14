using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Weapons
{ 
	public class EggBomb : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemsThatCountAsBombsForDemolitionistToSpawn[Type] = true;
            Item.ResearchUnlockCount = 99;
        }
        public override void SetDefaults()
		{
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shootSpeed = 9f;
            Item.shoot = PMProjID.EggBomb;
            Item.width = 40;
            Item.height = 40;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.UseSound = SoundID.Item1;
            Item.useTime = 40;
            Item.useAnimation = Item.useTime;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.value = Item.sellPrice(silver: 10);
            Item.rare = ItemRarityID.Orange;
        }
	}
}
