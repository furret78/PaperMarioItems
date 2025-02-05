using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.ConsumablesSPM
{ 
	public class SuperMushroomShake : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.FoodParticleColors[Type] = [
                new(41, 138, 214),
                new(231, 227, 231),
                new(255, 215, 132)
            ];
            Item.ResearchUnlockCount = 150;
        }

        public override void SetDefaults()
        {
            Item.DefaultToFood(31, 39, 0, 0, true);
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 45);
            Item.healLife = 50;
            Item.potion = true;
        }

        public override void OnConsumeItem(Player player)
        {
            player.TryToResetHungerToNeutral();
            player.ClearBuff(BuffID.Poisoned);
        }

        //detour
        public override void Load()
        {
            On_Player.ApplyPotionDelay += On_Player_ApplyPotionDelay;
        }
        private void On_Player_ApplyPotionDelay(On_Player.orig_ApplyPotionDelay orig, Player self, Item sItem)
        {
            if (sItem.type == Type)
            {
                int delay = 240;
                if (self.pStone) delay = (int)(delay * Player.PhilosopherStoneDurationMultiplier);
                self.AddBuff(21, delay);
            }
            else orig(self, sItem);
        }

        public override void AddRecipes()
		{
            Recipe recipe = CreateRecipe()
                .AddIngredient(PMItemID.SuperMushroom)
                .AddIngredient(PMItemID.TastyTonic)
                .AddTile(TileID.Bottles)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient(PMItemID.SuperMushroom)
                .AddIngredient(PMItemID.FreshJuice)
                .AddTile(TileID.Bottles)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient(PMItemID.SuperMushroom)
                .AddIngredient(PMItemID.TastyTonic)
                .AddIngredient(ItemID.Bottle)
                .AddTile(TileID.WorkBenches)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient(PMItemID.SuperMushroom)
                .AddIngredient(PMItemID.FreshJuice)
                .AddIngredient(ItemID.Bottle)
                .AddTile(TileID.WorkBenches)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient(PMItemID.MushroomShake)
                .AddIngredient(ItemID.LesserHealingPotion, 3)
                .AddTile(TileID.WorkBenches)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient(PMItemID.MushroomShake)
                .AddIngredient(ItemID.HealingPotion)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
