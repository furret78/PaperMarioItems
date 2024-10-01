using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{
	public class SpitePouch : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.RuinPowder;
        }

        public override void SetDefaults()
		{
			Item.width = 39;
			Item.height = 40;
			Item.useTurn = true;
			Item.useTime = 17;
			Item.useAnimation = Item.useTime;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = PMSoundID.useItem;
			Item.consumable = true;
			Item.maxStack = Item.CommonMaxStack;
			Item.rare = ItemRarityID.Orange;
			Item.value = Item.buyPrice(silver: 20);
			Item.buffType = PMBuffID.Payback;
			Item.buffTime = 7200;
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(2)
				.AddIngredient(ItemID.ThornsPotion)
				.AddIngredient(ItemID.VilePowder)
				.AddTile(TileID.WorkBenches)
				.Register();
            recipe = CreateRecipe(2)
                .AddIngredient(ItemID.ThornsPotion)
                .AddIngredient(ItemID.ViciousPowder)
                .AddTile(TileID.WorkBenches)
                .Register();
            recipe = CreateRecipe(4)
                .AddIngredient(ItemID.ThornsPotion)
                .AddIngredient(ItemID.PurificationPowder)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
	}
}
