using Microsoft.Xna.Framework;
using PaperMarioItems.Common.Players;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{
	public class ShootingStar : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.ThunderRage;
            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults()
		{
            Item.width = 40;
            Item.height = 40;
            Item.useTurn = true;
            Item.useTime = 17;
            Item.useAnimation = Item.useTime;
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.buyPrice(silver: 30);
        }

        public override bool? UseItem(Player player)
        {
            if (player.GetModPlayer<PaperPlayer>().shootingStar <= 0 && !player.GetModPlayer<PaperPlayer>().shootingStarActive)
            {
                SoundEngine.PlaySound(PMSoundID.useItem, player.Center);
                player.GetModPlayer<PaperPlayer>().shootingStar = 18;
                return true;
            }
            else return false;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.WhiteSmoke.ToVector3() * 0.55f * Main.essScale);
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe()
				.AddIngredient(ItemID.FallenStar, 10)
                .AddIngredient(ItemID.Meteorite, 10)
                .AddIngredient(ItemID.SoulofFlight, 5)
				.AddTile(TileID.WorkBenches)
				.Register();
            recipe = CreateRecipe()
                .AddIngredient(ItemID.ManaCrystal, 2)
                .AddIngredient(ItemID.Meteorite, 10)
                .AddIngredient(ItemID.SoulofFlight, 5)
                .AddTile(TileID.WorkBenches)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient(ItemID.FallenStar, 10)
                .AddIngredient(ItemID.Meteorite, 10)
                .AddIngredient(RecipeGroupID.Fragment, 4)
                .AddTile(TileID.WorkBenches)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient(ItemID.ManaCrystal, 2)
                .AddIngredient(ItemID.Meteorite, 10)
                .AddRecipeGroup(RecipeGroupID.Fragment, 4)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
	}
}
