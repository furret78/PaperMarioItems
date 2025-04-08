using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class CakeMix : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.FoodParticleColors[Type] = [
                new(255, 255, 156)
            ];
            Item.ResearchUnlockCount = 500;
        }

        public override void SetDefaults()
		{
            Item.DefaultToFood(40, 37, 0, 0);
            Item.rare = ItemRarityID.White;
            Item.value = Item.buyPrice(silver: 2, copper: 50);
            Item.healMana = 5;
        }

        public override void Load()
        {
            On_Player.ApplyLifeAndOrMana += On_Player_ApplyLifeAndOrMana;
        }
        private static void On_Player_ApplyLifeAndOrMana(On_Player.orig_ApplyLifeAndOrMana orig, Player self, Item sItem)
        {
            if (sItem.type == PMItemID.CakeMix)
            {
                int healMana = sItem.healMana;
                self.statMana += healMana;
                if (self.statMana > self.statManaMax2) self.statMana = self.statManaMax2;
                if (Main.myPlayer == self.whoAmI) self.ManaEffect(healMana);
            }
            else orig(self, sItem);
        }
        public override void OnConsumeItem(Player player)
        {
            player.TryToResetHungerToNeutral();
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(3)
				.AddIngredient(ItemID.Hay, 3)
                .AddIngredient(RecipeGroupID.Fruit)
				.AddTile(TileID.WorkBenches)
                .DisableDecraft()
				.Register();
            recipe = CreateRecipe(5)
                .AddIngredient(ItemID.Hay, 3)
                .AddIngredient(ItemID.EucaluptusSap)
                .AddTile(TileID.WorkBenches)
                .DisableDecraft()
                .Register();
            recipe = CreateRecipe()
                .AddIngredient(ItemID.Hay, 6)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
	}
}
