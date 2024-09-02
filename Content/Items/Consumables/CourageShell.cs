using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class CourageShell : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 39;
			Item.height = 38;
			Item.useTurn = true;
			Item.useTime = 17;
			Item.useAnimation = Item.useTime;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = PaperMarioItems.useItemPM;
			Item.consumable = true;
			Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(silver: 5);
			Item.buffType = BuffID.Ironskin;
			Item.buffTime = 18000;
        }

        public override void OnConsumeItem(Player player)
        {
			player.AddBuff(BuffID.Endurance, Item.buffTime);
        }
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(3)
				.AddIngredient(RecipeGroupID.Turtles)
				.AddIngredient(ItemID.IronskinPotion)
                .AddIngredient(ItemID.EndurancePotion)
                .AddTile(TileID.WorkBenches)
				.Register();
            recipe = CreateRecipe()
                .AddIngredient(ItemID.TurtleShell, 2)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
	}
}
