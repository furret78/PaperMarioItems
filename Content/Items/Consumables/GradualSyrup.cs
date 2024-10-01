using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class GradualSyrup : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.DrinkParticleColors[Type] = [
                new(255, 138, 156),
                new(255, 227, 231)
            ];
            Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
		{
            Item.DefaultToFood(40, 40, BuffID.ManaRegeneration, 36000, true);
            Item.rare = ItemRarityID.White;
            Item.value = Item.buyPrice(silver: 15);
        }
        public override void OnConsumeItem(Player player)
        {
            player.TryToResetHungerToNeutral();
        }
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(2)
				.AddIngredient<HoneySyrup>(2)
                .AddIngredient(ItemID.ManaRegenerationPotion)
                .AddTile(TileID.WorkBenches)
                .Register();
            recipe = CreateRecipe(3)
                .AddIngredient<MapleSyrup>()
                .AddIngredient(ItemID.ManaRegenerationPotion)
                .AddTile(TileID.WorkBenches)
                .Register();
            recipe = CreateRecipe(5)
                .AddIngredient<JamminJelly>()
                .AddIngredient(ItemID.ManaRegenerationPotion)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
	}
}
