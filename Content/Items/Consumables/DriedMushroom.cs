using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class DriedMushroom : ModItem
	{
        public override void SetDefaults()
        {
            Item.width = 33;
            Item.height = 38;
            Item.useTurn = true;
            Item.useTime = 17;
            Item.useAnimation = Item.useTime;
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.UseSound = SoundID.Item2;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.White;
            Item.value = Item.buyPrice(silver: 1);
            Item.healLife = 5;
            Item.potion = true;
        }
        public override void OnConsumeItem(Player player)
        {
            player.TryToResetHungerToNeutral();
        }
        readonly int customPotionTime = 5;
        public override void Load()
        {
            On_Player.ApplyPotionDelay += On_Player_ApplyPotionDelay;
        }

        private void On_Player_ApplyPotionDelay(On_Player.orig_ApplyPotionDelay orig, Player self, Item sItem)
        {
            if (sItem.type == Type)
            {
                int delay = customPotionTime * 60;
                if (self.pStone) delay = (int)((float)delay * Player.PhilosopherStoneDurationMultiplier);
                self.AddBuff(21, delay);
            }
            else orig(self, sItem);
        }
        public override void AddRecipes()
		{
            //furnaces
            Recipe recipe = CreateRecipe()
                .AddIngredient<Mushroom>()
                .AddTile(TileID.Furnaces)
                .Register();
            recipe = CreateRecipe(3)
                .AddIngredient<SuperMushroom>()
                .AddTile(TileID.Furnaces)
                .Register();
            recipe = CreateRecipe(5)
                .AddIngredient<UltraMushroom>()
                .AddTile(TileID.Furnaces)
                .Register();
            //campfire
            recipe = CreateRecipe(2)
                .AddRecipeGroup(nameof(ItemID.Mushroom))
                .AddIngredient(ItemID.LesserHealingPotion)
                .AddTile(TileID.Campfire)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient<Mushroom>()
                .AddTile(TileID.Campfire)
                .Register();
            recipe = CreateRecipe(3)
                .AddIngredient<SuperMushroom>()
                .AddTile(TileID.Campfire)
                .Register();
            recipe = CreateRecipe(5)
                .AddIngredient<UltraMushroom>()
                .AddTile(TileID.Campfire)
                .Register();
            //near lava
            recipe = CreateRecipe(2)
                .AddRecipeGroup(nameof(ItemID.Mushroom))
                .AddIngredient(ItemID.LesserHealingPotion)
                .AddCondition(Condition.NearLava)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient<Mushroom>()
                .AddCondition(Condition.NearLava)
                .Register();
            recipe = CreateRecipe(3)
                .AddIngredient<SuperMushroom>()
                .AddCondition(Condition.NearLava)
                .Register();
            recipe = CreateRecipe(5)
                .AddIngredient<UltraMushroom>()
                .AddCondition(Condition.NearLava)
                .Register();
        }
    }
}
