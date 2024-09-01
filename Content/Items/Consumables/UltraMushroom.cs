using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class UltraMushroom : ModItem
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
            Item.value = Item.buyPrice(gold: 2);
            Item.healLife = 75;
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
                int delay = customPotionTime * 60; // see customPotionTime above (seconds)
                if (self.pStone) delay = (int)((float)delay * Player.PhilosopherStoneDurationMultiplier);
                self.AddBuff(21, delay);
            }
            else orig(self, sItem);
        }
        public override void AddRecipes()
		{
            Recipe recipe = CreateRecipe()
                .AddIngredient<SuperMushroom>()
                .AddIngredient(ItemID.HealingPotion, 2)
				.Register();
            recipe = CreateRecipe()
                .AddIngredient<SuperMushroom>()
                .AddIngredient(ItemID.GreaterHealingPotion)
                .Register();
            recipe = CreateRecipe(3)
                .AddRecipeGroup(nameof(ItemID.Mushroom), 9)
                .AddIngredient(ItemID.SuperHealingPotion)
                .Register();
        }
    }
}
