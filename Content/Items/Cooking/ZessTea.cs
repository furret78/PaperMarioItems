using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using PaperMarioItems.Content.Items.Consumables;

namespace PaperMarioItems.Content.Items.Cooking
{ 
	public class ZessTea : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.DrinkParticleColors[Type] = [new(255, 186, 0)];
            Item.ResearchUnlockCount = 300;
        }

        public override void SetDefaults()
		{
            Item.DefaultToFood(37, 37, 0, 0, true);
            Item.rare = ItemRarityID.LightPurple;
            Item.value = Item.sellPrice(silver: 10);
            Item.healMana = 100;
        }

        public override void OnConsumeItem(Player player)
		{
            player.TryToResetHungerToNeutral();
        }

        public override void Load()
        {
            On_Player.ApplyLifeAndOrMana += On_Player_ApplyLifeAndOrMana;
        }

        private static void On_Player_ApplyLifeAndOrMana(On_Player.orig_ApplyLifeAndOrMana orig, Player player, Item sItem)
        {
            if (sItem.type == PMItemID.ZessTea)
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
				.AddIngredient<GoldenLeaf>()
                .AddTile(TileID.CookingPots)
				.Register();
        }
	}
}
