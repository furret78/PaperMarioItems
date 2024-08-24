using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using PaperMarioItems.Content.Items.Consumables;

namespace PaperMarioItems.Content.Items.Cooking
{ 
	public class MushroomSteak : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 36;
			Item.useTurn = true;
			Item.useTime = 17;
			Item.useAnimation = Item.useTime;
			Item.useStyle = ItemUseStyleID.EatFood;
			Item.UseSound = SoundID.Item2;
			Item.consumable = true;
			Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(copper: 25);
			Item.buffType = BuffID.WellFed3;
			Item.buffTime = 21600;
            Item.healLife = 150;
            Item.healMana = 50;
            Item.potion = true;
        }
        public override void OnConsumeItem(Player player)
		{
            player.TryToResetHungerToNeutral();
        }
        public override void Load()
        {
            On_Player.ApplyPotionDelay += On_Player_ApplyPotionDelay;
            On_Player.ApplyLifeAndOrMana += On_Player_ApplyLifeAndOrMana;
        }

        private void On_Player_ApplyPotionDelay(On_Player.orig_ApplyPotionDelay orig, Player player, Item sItem)
        {
            if (sItem.type == Type) return;
            else orig(player, sItem);
        }
        private void On_Player_ApplyLifeAndOrMana(On_Player.orig_ApplyLifeAndOrMana orig, Player player, Item sItem)
        {
            if (sItem.type == Type)
            {
                int num = sItem.healLife; int num2 = sItem.healMana;
                player.statLife += num; player.statMana += num2;
                if (player.statLife > player.statLifeMax2) player.statLife = player.statLifeMax2;
                if (player.statMana > player.statManaMax2) player.statMana = player.statManaMax2;
                if (num > 0 && Main.myPlayer == player.whoAmI) player.HealEffect(num);
                if (num2 > 0)
                {
                    if (Main.myPlayer == player.whoAmI) player.ManaEffect(num2);
                }
            }
            else orig(player, sItem);
        }
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe()
                .AddIngredient<DriedMushroom>()
                .AddIngredient<LifeMushroom>()
                .AddCondition(PaperMarioConditions.HasCookbook)
                .AddTile(TileID.CookingPots)
				.Register();
            recipe = CreateRecipe()
                .AddIngredient<DriedMushroom>()
                .AddIngredient<UltraMushroom>()
                .AddCondition(PaperMarioConditions.HasCookbook)
                .AddTile(TileID.CookingPots)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient<Mushroom>()
                .AddIngredient<LifeMushroom>()
                .AddCondition(PaperMarioConditions.HasCookbook)
                .AddTile(TileID.CookingPots)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient<Mushroom>()
                .AddIngredient<UltraMushroom>()
                .AddCondition(PaperMarioConditions.HasCookbook)
                .AddTile(TileID.CookingPots)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient<SuperMushroom>()
                .AddIngredient<LifeMushroom>()
                .AddCondition(PaperMarioConditions.HasCookbook)
                .AddTile(TileID.CookingPots)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient<SuperMushroom>()
                .AddIngredient<UltraMushroom>()
                .AddCondition(PaperMarioConditions.HasCookbook)
                .AddTile(TileID.CookingPots)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient<UltraMushroom>()
                .AddIngredient<LifeMushroom>()
                .AddCondition(PaperMarioConditions.HasCookbook)
                .AddTile(TileID.CookingPots)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient<VoltMushroom>()
                .AddIngredient<UltraMushroom>()
                .AddCondition(PaperMarioConditions.HasCookbook)
                .AddTile(TileID.CookingPots)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient<GoldenLeaf>()
                .AddIngredient<LifeMushroom>()
                .AddCondition(PaperMarioConditions.HasCookbook)
                .AddTile(TileID.CookingPots)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient<GoldenLeaf>()
                .AddIngredient<UltraMushroom>()
                .AddCondition(PaperMarioConditions.HasCookbook)
                .AddTile(TileID.CookingPots)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient<TurtleyLeaf>()
                .AddIngredient<LifeMushroom>()
                .AddCondition(PaperMarioConditions.HasCookbook)
                .AddTile(TileID.CookingPots)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient<TurtleyLeaf>()
                .AddIngredient<UltraMushroom>()
                .AddCondition(PaperMarioConditions.HasCookbook)
                .AddTile(TileID.CookingPots)
                .Register();
            //non-cookbook
            recipe = CreateRecipe()
                .AddIngredient<UltraMushroom>()
                .AddTile(TileID.CookingPots)
                .Register();
        }
	}
}
