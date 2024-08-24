using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class SuperMushroom : ModItem
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
            Item.value = Item.buyPrice(silver: 15);
            Item.healLife = 50;
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
            Recipe recipe = CreateRecipe()
                .AddIngredient<Mushroom>()
                .AddIngredient(ItemID.LesserHealingPotion, 3)
                .AddTile(TileID.WorkBenches)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient<Mushroom>()
                .AddIngredient(ItemID.HealingPotion)
                .AddTile(TileID.WorkBenches)
                .Register();
            recipe = CreateRecipe()
                .AddRecipeGroup(nameof(ItemID.Mushroom), 3)
                .AddIngredient(ItemID.HealingPotion)
                .AddTile(TileID.WorkBenches)
                .Register();
            //cooking
            recipe = CreateRecipe()
                .AddIngredient<PointSwap>()
                .AddIngredient<LifeMushroom>()
                .AddCondition(PaperMarioConditions.HasCookbook)
                .AddTile(TileID.CookingPots)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient<PointSwap>()
                .AddIngredient<MapleSyrup>()
                .AddCondition(PaperMarioConditions.HasCookbook)
                .AddTile(TileID.CookingPots)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient<PointSwap>()
                .AddIngredient<VoltMushroom>()
                .AddCondition(PaperMarioConditions.HasCookbook)
                .AddTile(TileID.CookingPots)
                .Register();
        }
    }
}
