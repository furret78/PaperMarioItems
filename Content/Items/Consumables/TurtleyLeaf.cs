using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class TurtleyLeaf : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.FoodParticleColors[Type] = [new(74, 195, 74), new(24, 138, 24)];
            Item.ResearchUnlockCount = 500;
        }

        public override void SetDefaults()
		{
            Item.DefaultToFood(40, 38, 0, 0);
            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(silver: 2);
            Item.healMana = 15;
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
				.AddIngredient(ItemID.Waterleaf, 3)
                .AddTile(TileID.WorkBenches)
				.Register();
        }
	}
}
