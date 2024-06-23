using PaperMarioItems.Content.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class HotSauce : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 40;
			Item.useTurn = true;
			Item.useTime = 17;
			Item.useAnimation = Item.useTime;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item3;
			Item.consumable = true;
			Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(copper: 25);
			Item.buffType = ModContent.BuffType<ChargedBuff>();
			Item.buffTime = 7200;
        }
        public override void OnConsumeItem(Player player)
		{
			//SoundEngine.PlaySound(SoundID.Item3);
            player.TryToResetHungerToNeutral();
        }
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(1)
				.AddIngredient(ItemID.SpicyPepper, 1)
                .AddIngredient(ItemID.WrathPotion, 1)
                .AddTile(TileID.WorkBenches)
				.Register();
            recipe = CreateRecipe(3)
                .AddIngredient(ItemID.SpicyPepper, 1)
                .AddIngredient(ItemID.BottledWater, 3)
                .AddTile(TileID.CookingPots)
                .Register();
            recipe = CreateRecipe(3)
                .AddIngredient(ItemID.SpicyPepper, 1)
                .AddCondition(Condition.NearWater)
                .AddTile(TileID.CookingPots)
                .Register();
            recipe = CreateRecipe(3)
                .AddIngredient(ItemID.SpicyPepper, 1)
                .AddIngredient(ItemID.WaterBucket)
                .AddTile(TileID.CookingPots)
                .AddTile(TileID.Bottles)
                .Register();
        }
	}
}
