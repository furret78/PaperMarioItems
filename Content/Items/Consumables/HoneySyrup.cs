using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class HoneySyrup : ModItem
	{
		public override void SetDefaults()
		{
            Item.width = 40;
            Item.height = 40;
            Item.useTurn = true;
            Item.useTime = 17;
            Item.useAnimation = Item.useTime;
            Item.consumable = true;
            Item.UseSound = SoundID.Item3;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.White;
            Item.value = Item.buyPrice(copper: 20);
            Item.buffType = BuffID.Honey;
            Item.buffTime = 1800;
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
            //with honey bucket
			Recipe recipe = CreateRecipe(6)
				.AddIngredient(ItemID.HoneyBucket)
                .AddIngredient(ItemID.WaterBucket)
                .AddTile(TileID.Bottles)
				.Register();
            recipe = CreateRecipe(3)
                .AddIngredient(ItemID.HoneyBucket)
                .AddIngredient(ItemID.BottledWater, 3)
                .AddTile(TileID.WorkBenches)
                .Register();
            recipe = CreateRecipe(3)
                .AddIngredient(ItemID.HoneyBucket)
                .AddCondition(Condition.NearWater)
                .AddTile(TileID.Bottles)
                .Register();
            recipe = CreateRecipe(3)
                .AddIngredient(ItemID.HoneyBucket)
                .AddIngredient(ItemID.LesserManaPotion, 3)
                .AddTile(TileID.Bottles)
                .Register();
            recipe = CreateRecipe(3)
                .AddIngredient(ItemID.HoneyBucket)
                .AddIngredient(ItemID.ManaPotion)
                .AddTile(TileID.Bottles)
                .Register();
            //with bottled honey
            recipe = CreateRecipe(3)
                .AddIngredient(ItemID.BottledHoney, 3)
                .AddIngredient(ItemID.WaterBucket)
                .AddTile(TileID.WorkBenches)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient(ItemID.BottledHoney)
                .AddIngredient(ItemID.BottledWater)
                .AddTile(TileID.WorkBenches)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient(ItemID.BottledHoney)
                .AddCondition(Condition.NearWater)
                .AddTile(TileID.Bottles)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient(ItemID.BottledHoney)
                .AddIngredient(ItemID.LesserManaPotion)
                .AddTile(TileID.WorkBenches)
                .Register();
            recipe = CreateRecipe(3)
                .AddIngredient(ItemID.BottledHoney, 3)
                .AddIngredient(ItemID.ManaPotion)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
	}
}
