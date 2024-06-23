using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class DriedBouquet : ModItem
	{
        public override void SetDefaults()
        {
            Item.width = 39;
            Item.height = 37;
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
        public override void Load()
        {
            On_Player.ApplyPotionDelay += On_Player_ApplyPotionDelay;
        }
        private void On_Player_ApplyPotionDelay(On_Player.orig_ApplyPotionDelay orig, Player self, Item sItem)
        {
            if (sItem.type == Type)
            {
                int delay = (int)(2.5 * 60);
                if (self.pStone) delay = (int)((float)delay * Player.PhilosopherStoneDurationMultiplier);
                self.AddBuff(21, delay);
            }
            else orig(self, sItem);
        }
        public override void AddRecipes()
		{
            Recipe recipe = CreateRecipe()
                .AddRecipeGroup(Language.GetTextValue($"Mods.PaperMarioItems.Content.FlowerGroup"), 3)
                .AddTile(TileID.Furnaces)
				.Register();
            recipe = CreateRecipe()
                .AddRecipeGroup(Language.GetTextValue($"Mods.PaperMarioItems.Content.FlowerGroup"), 3)
                .AddCondition(Condition.NearLava)
                .Register();
            recipe = CreateRecipe()
                .AddRecipeGroup(Language.GetTextValue($"Mods.PaperMarioItems.Content.FlowerGroup"), 3)
                .AddTile(TileID.Campfire)
                .Register();
        }
    }
}
