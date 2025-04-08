using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using PaperMarioItems.Content.Items.Consumables;

namespace PaperMarioItems.Content.Items.Cooking
{ 
	public class FriedEgg : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.MysticEgg;
            ItemID.Sets.FoodParticleColors[Type] = [
                new(255, 186, 0),
                new(231, 227, 231)
            ];
            Item.ResearchUnlockCount = 50;
        }

        public override void SetDefaults()
		{
            Item.DefaultToFood(40, 29, BuffID.WellFed, 7200);
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(silver: 7);
            Item.healLife = 30;
            Item.potion = true;
        }
        public override void OnConsumeItem(Player player)
		{
            player.TryToResetHungerToNeutral();
        }
        public override void Load()
        {
            On_Player.ApplyPotionDelay += On_Player_ApplyPotionDelay;
        }

        private static void On_Player_ApplyPotionDelay(On_Player.orig_ApplyPotionDelay orig, Player player, Item sItem)
        {
            if (sItem.type == PMItemID.FriedEgg) return;
            else orig(player, sItem);
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe()
                .AddIngredient<MysticEgg>()
                .AddTile(TileID.CookingPots)
                .Register();
        }
	}
}