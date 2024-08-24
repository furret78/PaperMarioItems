using PaperMarioItems.Content.Items.Cooking;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class Mushroom : ModItem
	{
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.useTurn = true;
            Item.useTime = 17;
            Item.useAnimation = Item.useTime;
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.UseSound = SoundID.Item2;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.White;
            Item.value = Item.buyPrice(silver: 1);
            Item.healLife = 25;
            Item.potion = true;
        }
        public override void OnConsumeItem(Player player)
        {
            player.TryToResetHungerToNeutral();
        }
        readonly int customPotionTime = 5;
        //detour
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
            Recipe recipe = CreateRecipe(2)
                .AddRecipeGroup(nameof(ItemID.Mushroom))
                .AddIngredient(ItemID.LesserHealingPotion)
                .AddTile(TileID.WorkBenches)
                .Register();
            //cooking
            recipe = CreateRecipe()
                   .AddIngredient<PointSwap>()
                   .AddIngredient<HoneySyrup>()
                   .AddCondition(PaperMarioConditions.HasCookbook)
                   .AddTile(TileID.CookingPots)
                   .Register();
            recipe = CreateRecipe()
                   .AddIngredient<PointSwap>()
                   .AddIngredient(ItemID.GoldBar)
                   .AddCondition(PaperMarioConditions.HasCookbook)
                   .AddTile(TileID.CookingPots)
                   .Register();
            recipe = CreateRecipe()
                   .AddIngredient<PointSwap>()
                   .AddIngredient<DriedMushroom>()
                   .AddCondition(PaperMarioConditions.HasCookbook)
                   .AddTile(TileID.CookingPots)
                   .Register();
            recipe = CreateRecipe()
                   .AddIngredient<PointSwap>()
                   .AddIngredient<PoisonMushroom>()
                   .AddCondition(PaperMarioConditions.HasCookbook)
                   .AddTile(TileID.CookingPots)
                   .Register();
            recipe = CreateRecipe()
                   .AddIngredient<DriedMushroom>()
                   .AddIngredient<HotSauce>()
                   .AddCondition(PaperMarioConditions.HasCookbook)
                   .AddTile(TileID.CookingPots)
                   .Register();
            recipe = CreateRecipe()
                   .AddIngredient<DriedMushroom>()
                   .AddIngredient(ItemID.Peach)
                   .AddCondition(PaperMarioConditions.HasCookbook)
                   .AddTile(TileID.CookingPots)
                   .Register();
            recipe = CreateRecipe()
                   .AddIngredient<DriedMushroom>()
                   .AddIngredient<PowerPunch>()
                   .AddCondition(PaperMarioConditions.HasCookbook)
                   .AddTile(TileID.CookingPots)
                   .Register();
        }
    }
}
