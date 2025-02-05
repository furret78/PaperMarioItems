using PaperMarioItems.Common.Players;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.ConsumablesSPM
{
	public class PowerPlus : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.HPPlus;
            Item.ResearchUnlockCount = 500;
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
            Item.value = Item.sellPrice(gold: 4);
        }

        public override bool? UseItem(Player player)
        {
            var modPlayer = player.GetModPlayer<PaperPlayer>();
            modPlayer.IncreasePowerPlus(player);
            return true;
        }

        public override void AddRecipes()
		{
            Recipe recipe = CreateRecipe(3)
                .AddIngredient(ItemID.AvengerEmblem)
                .AddIngredient(PMItemID.HPPlus)
                .AddTile(TileID.Anvils)
                .Register();
            recipe = CreateRecipe(4)
               .AddIngredient(ItemID.DestroyerEmblem)
               .AddIngredient(PMItemID.HPPlus)
               .AddTile(TileID.Anvils)
               .Register();
        }
	}
}
