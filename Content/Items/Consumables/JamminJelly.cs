using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class JamminJelly : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.UltraMushroom;
            ItemID.Sets.DrinkParticleColors[Type] = [
                new(255, 199, 49),
                new(231, 158, 29)
            ];
            Item.ResearchUnlockCount = 20;
        }

        public override void SetDefaults()
		{
            Item.DefaultToFood(36, 40, 0, 0, true);
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(gold: 2);
            Item.healMana = 75;
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
            if (sItem.type == PMItemID.JamminJelly)
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
			Recipe recipe = CreateRecipe(4)
				.AddIngredient<MapleSyrup>()
                .AddIngredient(ItemID.SuperManaPotion)
                .AddTile(TileID.WorkBenches)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient(ItemID.BottledHoney)
                .AddIngredient(ItemID.SuperManaPotion)
                .AddTile(TileID.WorkBenches)
                .Register();
            recipe = CreateRecipe(2)
                .AddIngredient(ItemID.HoneyBucket)
                .AddIngredient(ItemID.SuperManaPotion)
                .AddTile(TileID.Bottles)
                .Register();
        }
	}
}
