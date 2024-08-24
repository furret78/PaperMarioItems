using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using PaperMarioItems.Content.Items.Consumables;
using Terraria.Localization;

namespace PaperMarioItems.Content.Items.Cooking
{ 
	public class FreshJuice : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 39;
			Item.height = 40;
			Item.useTurn = true;
			Item.useTime = 17;
			Item.useAnimation = Item.useTime;
			Item.useStyle = ItemUseStyleID.DrinkLiquid;
			Item.UseSound = SoundID.Item3;
			Item.consumable = true;
			Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(copper: 25);
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
        private void On_Player_ApplyLifeAndOrMana(On_Player.orig_ApplyLifeAndOrMana orig, Player player, Item sItem)
        {
            if (sItem.type == Type)
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
                .AddIngredient(ItemID.Coconut)
                .AddIngredient(ItemID.Peach)
                .AddCondition(PaperMarioConditions.HasCookbook)
                .AddTile(TileID.CookingPots)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient(ItemID.Coconut)
                .AddIngredient(ItemID.Mango)
                .AddCondition(PaperMarioConditions.HasCookbook)
                .AddTile(TileID.CookingPots)
                .Register();
            //non-cooking
            recipe = CreateRecipe()
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
                .AddIngredient<GradualSyrup>()
                .AddTile(TileID.CookingPots)
                .Register();
        }
	}
}
