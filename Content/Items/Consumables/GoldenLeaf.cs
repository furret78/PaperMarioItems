using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class GoldenLeaf : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.FoodParticleColors[Type] = [
                new(255, 190, 0),
                new(206, 150, 0),
                new(165, 116, 0)
            ];
            Item.ResearchUnlockCount = 350;
        }

        public override void SetDefaults()
		{
            Item.DefaultToFood(39, 40, 0, 0);
            Item.rare = ItemRarityID.White;
            Item.value = Item.buyPrice(silver: 3);
            Item.healMana = 25;
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
			Recipe recipe = CreateRecipe()
				.AddIngredient(ItemID.GrassSeeds, 5)
				.Register();
            recipe = CreateRecipe()
                .AddIngredient(ItemID.HallowedSeeds, 3)
                .Register();
        }
	}
}
