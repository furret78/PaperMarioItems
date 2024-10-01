using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class MapleSyrup : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.SuperMushroom;
            ItemID.Sets.DrinkParticleColors[Type] = [
                new(255, 166, 0),
                new(208, 126, 0),
                new(181, 105, 0)
            ];
            Item.ResearchUnlockCount = 50;
        }

        public override void SetDefaults()
		{
            Item.DefaultToFood(40, 40, 0, 0, true);
            Item.rare = ItemRarityID.White;
            Item.value = Item.buyPrice(silver: 15);
            Item.healMana = 50;
        }

        public override void OnConsumeItem(Player player)
        {
            player.TryToResetHungerToNeutral();
        }

        public override void Load()
        {
            On_Player.ApplyLifeAndOrMana += On_Player_ApplyLifeAndOrMana;
        }
        private void On_Player_ApplyLifeAndOrMana(On_Player.orig_ApplyLifeAndOrMana orig, Player self, Item sItem)
        {
            if (sItem.type == Type)
            {
                int healMana = sItem.healMana;
                self.statMana += healMana;
                if (self.statMana > self.statManaMax2) self.statMana = self.statManaMax2;
                if (Main.myPlayer == self.whoAmI) self.ManaEffect(healMana);
            }
            else orig(self, sItem);
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(4)
				.AddIngredient<HoneySyrup>(2)
                .AddIngredient<GoldenLeaf>()
                .Register();
            recipe = CreateRecipe()
                .AddIngredient<HoneySyrup>()
                .AddRecipeGroup(RecipeGroupID.Wood, 5)
                .AddTile(TileID.WorkBenches)
                .Register();
            recipe = CreateRecipe(3)
                .AddIngredient<HoneySyrup>()
                .AddIngredient(ItemID.GreaterManaPotion)
                .AddTile(TileID.WorkBenches)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient(ItemID.BottledHoney, 3)
                .AddIngredient(ItemID.GreaterManaPotion)
                .AddTile(TileID.WorkBenches)
                .Register();
            recipe = CreateRecipe(2)
                .AddIngredient(ItemID.HoneyBucket)
                .AddIngredient(ItemID.GreaterManaPotion)
                .AddTile(TileID.Bottles)
                .Register();
        }
	}
}
