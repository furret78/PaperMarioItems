using PaperMarioItems.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class HotSauce : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 7;
			Item.height = 24;
			Item.useTurn = true;
			Item.useTime = 17;
			Item.useAnimation = Item.useTime;
			Item.useStyle = ItemUseStyleID.DrinkLiquid;
			Item.UseSound = SoundID.Item3;
			Item.consumable = true;
			Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(silver: 5);
        }

        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<PaperPlayer>().DrinkHotSauce(player);
            return true;
        }

        public override void OnConsumeItem(Player player)
		{
			//SoundEngine.PlaySound(SoundID.Item3);
            player.TryToResetHungerToNeutral();
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe()
				.AddIngredient(ItemID.SpicyPepper)
                .AddIngredient(ItemID.WrathPotion)
                .AddTile(TileID.WorkBenches)
				.Register();
            recipe = CreateRecipe(3)
                .AddIngredient(ItemID.SpicyPepper)
                .AddIngredient(ItemID.BottledWater, 3)
                .AddTile(TileID.CookingPots)
                .Register();
            recipe = CreateRecipe(3)
                .AddIngredient(ItemID.SpicyPepper)
                .AddCondition(Condition.NearWater)
                .AddTile(TileID.CookingPots)
                .Register();
            recipe = CreateRecipe(3)
                .AddIngredient(ItemID.SpicyPepper)
                .AddIngredient(ItemID.WaterBucket)
                .AddTile(TileID.CookingPots)
                .AddTile(TileID.Bottles)
                .Register();
        }
	}
}
