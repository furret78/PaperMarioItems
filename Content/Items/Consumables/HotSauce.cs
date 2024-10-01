using PaperMarioItems.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class HotSauce : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.DrinkParticleColors[Type] = [new(255, 114, 69)];
            Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
		{
            Item.DefaultToFood(7, 24, 0, 0, true);
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
