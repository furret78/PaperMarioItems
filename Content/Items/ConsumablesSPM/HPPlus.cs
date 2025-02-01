using PaperMarioItems.Common.Players;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.ConsumablesSPM
{
	public class HPPlus : ModItem
	{
        const int HPPlusMax = 4;
        public const int HPPlusValue = 25;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
		{
            Item.width = 39;
            Item.height = 39;
            Item.useTurn = true;
            Item.useTime = 17;
            Item.useAnimation = Item.useTime;
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.buyPrice(gold: 2);
        }

        public override bool CanUseItem(Player player)
        {
            return player.ConsumedLifeCrystals >= Player.LifeCrystalMax && player.ConsumedLifeFruit >= Player.LifeFruitMax;
        }

        public override bool? UseItem(Player player)
        {
            var modPlayer = player.GetModPlayer<PaperPlayer>();

            if (modPlayer.consumedHPPlus >= HPPlusMax) return null;
            player.UseHealthMaxIncreasingItem(HPPlusValue);
            SoundEngine.PlaySound(SoundID.Item4, player.position);
            modPlayer.consumedHPPlus++;
            return true;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe()
                .AddIngredient(ItemID.LifeFruit, 2)
                .AddIngredient(ItemID.LifeCrystal)
				.AddTile(TileID.WorkBenches)
				.Register();
            recipe = CreateRecipe()
                .AddIngredient(ItemID.LifeFruit, 2)
                .AddIngredient(ItemID.LifeCrystal)
                .AddTile(TileID.Anvils)
                .Register();
        }
	}
}
