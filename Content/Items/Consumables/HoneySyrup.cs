using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class HoneySyrup : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.Mushroom;
            ItemID.Sets.DrinkParticleColors[Type] = [new(255, 224, 0)];
            Item.ResearchUnlockCount = 50;
        }

        public override void SetDefaults()
		{
            Item.DefaultToFood(40, 40, BuffID.Honey, 1800, true);
            Item.rare = ItemRarityID.White;
            Item.value = Item.buyPrice(silver: 5);
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
        private static void On_Player_ApplyLifeAndOrMana(On_Player.orig_ApplyLifeAndOrMana orig, Player self, Item sItem)
        {
            if (sItem.type == PMItemID.HoneySyrup)
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
                .AddTile(TileID.WorkBenches)
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
