using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class TurtleyLeaf : ModItem
	{
		public override void SetDefaults()
		{
            Item.width = 40;
            Item.height = 38;
            Item.useTurn = true;
            Item.useTime = 17;
            Item.useAnimation = Item.useTime;
            Item.consumable = true;
            Item.UseSound = SoundID.Item2;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.maxStack = Item.CommonMaxStack;
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
