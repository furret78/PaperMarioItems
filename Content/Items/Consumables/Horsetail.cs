using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class Horsetail : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.FoodParticleColors[Type] = [
                new(255, 186, 49),
                new(165, 81, 41),
                new(206, 101, 49),
                new(255, 207, 99)
            ];
            Item.ResearchUnlockCount = 200;
        }

        public override void SetDefaults()
        {
            Item.DefaultToFood(40, 40, 0, 0);
            Item.rare = ItemRarityID.White;
            Item.value = Item.buyPrice(silver: 1);
            Item.healLife = 12;
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
                int delay = 120;
                if (self.pStone) delay = (int)((float)delay * Player.PhilosopherStoneDurationMultiplier);
                self.AddBuff(21, delay);
            }
            else orig(self, sItem);
        }
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe()
                .AddRecipeGroup(Language.GetTextValue($"Mods.PaperMarioItems.Content.FlowerGroup"), 3)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
