using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace PaperMarioItems.Content.Items.Cooking
{ 
	public class FreshJuice : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.Mango;
            ItemID.Sets.DrinkParticleColors[Type] = [new(255, 198, 20)];
            Item.ResearchUnlockCount = 100;
        }
        public override void SetDefaults()
		{
            Item.DefaultToFood(39, 40, 0, 0, true);
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(silver: 3);
            Item.healMana = 25;
        }
        public override void OnConsumeItem(Player player)
		{
            if (player.HasBuff(BuffID.Poisoned)) player.ClearBuff(BuffID.Poisoned);
            player.TryToResetHungerToNeutral();
        }
        public override void Load()
        {
            On_Player.ApplyLifeAndOrMana += On_Player_ApplyLifeAndOrMana;
        }

        private static void On_Player_ApplyLifeAndOrMana(On_Player.orig_ApplyLifeAndOrMana orig, Player player, Item sItem)
        {
            if (sItem.type == PMItemID.FreshJuice)
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
                .AddIngredient(ItemID.Peach)
                .AddTile(TileID.CookingPots)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient(ItemID.Mango)
                .AddTile(TileID.CookingPots)
                .Register();
            recipe = CreateRecipe()
                .AddRecipeGroup(Language.GetTextValue($"Mods.PaperMarioItems.Content.SyrupGroup"))
                .AddTile(TileID.CookingPots)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient(PMItemID.GradualSyrup)
                .AddTile(TileID.CookingPots)
                .Register();
        }
	}
}
