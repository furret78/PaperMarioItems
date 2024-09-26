using PaperMarioItems.Common.Players;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class Stopwatch : ModItem
	{
        public override void SetDefaults()
		{
            Item.width = 36;
            Item.height = 40;
            Item.useTurn = true;
            Item.useTime = 17;
            Item.useAnimation = Item.useTime;
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.buyPrice(platinum: 1);
        }

        public override bool? UseItem(Player player)
        {
            if (!player.GetModPlayer<PaperPlayer>().stopwatchActive && player.GetModPlayer<PaperPlayer>().stopwatchCooldown <= 0)
            {
                player.GetModPlayer<PaperPlayer>().stopwatchCooldown = Item.useTime+1;
                player.GetModPlayer<PaperPlayer>().stopwatchActive = true;
                //SoundEngine.PlaySound(PMSoundID.stopwatch, player.Center);
                SoundEngine.PlaySound(PMSoundID.useItem, player.Center);
                return true;
            }
            else return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe()
                .AddRecipeGroup(RecipeGroupID.Fragment, 9)
                .AddIngredient(ItemID.TinWatch)
                .AddTile(TileID.Anvils)
                .Register();
            recipe = CreateRecipe()
                .AddRecipeGroup(RecipeGroupID.Fragment, 9)
                .AddIngredient(ItemID.CopperWatch)
                .AddTile(TileID.Anvils)
                .Register();
            recipe = CreateRecipe(2)
                .AddRecipeGroup(RecipeGroupID.Fragment, 6)
                .AddIngredient(ItemID.SilverWatch)
                .AddTile(TileID.Anvils)
                .Register();
            recipe = CreateRecipe(2)
                .AddRecipeGroup(RecipeGroupID.Fragment, 6)
                .AddIngredient(ItemID.TungstenWatch)
                .AddTile(TileID.Anvils)
                .Register();
            recipe = CreateRecipe(3)
                .AddRecipeGroup(RecipeGroupID.Fragment, 3)
                .AddIngredient(ItemID.GoldWatch)
                .AddTile(TileID.Anvils)
                .Register();
            recipe = CreateRecipe(3)
                .AddRecipeGroup(RecipeGroupID.Fragment, 3)
                .AddIngredient(ItemID.PlatinumWatch)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
