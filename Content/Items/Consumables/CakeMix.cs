using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class CakeMix : ModItem
	{
		public override void SetDefaults()
		{
            Item.width = 40;
            Item.height = 37;
            Item.useTurn = true;
            Item.useTime = 17;
            Item.useAnimation = Item.useTime;
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.UseSound = SoundID.Item2;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.White;
            Item.value = Item.buyPrice(silver: 2, copper: 50);
            Item.healMana = 5;
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
				.Register();
            recipe = CreateRecipe(5)
                .AddIngredient(ItemID.Hay, 3)
                .AddIngredient(ItemID.EucaluptusSap)
                .AddTile(TileID.WorkBenches)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient(ItemID.Hay, 6)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
	}
}
