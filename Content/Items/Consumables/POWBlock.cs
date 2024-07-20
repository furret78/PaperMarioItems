using PaperMarioItems.Common.Players;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class POWBlock : ModItem
	{
        public const int powDamage = 20;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(powDamage);
        public override void SetDefaults()
		{
            Item.width = 40;
            Item.height = 40;
            Item.useTurn = true;
            Item.useTime = 20;
            Item.useAnimation = Item.useTime;
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = PaperMarioItems.useItemPM;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.White;
            Item.value = Item.buyPrice(copper: 25);
        }

        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<PaperPlayer>().PowBlock(player, powDamage);
            return true;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe()
				.AddIngredient(ItemID.Fireblossom, 3)
                .AddIngredient(ItemID.PixieDust, 3)
				.AddTile(TileID.WorkBenches)
				.Register();
            recipe = CreateRecipe(3)
                .AddIngredient(ItemID.FlowerofFire)
                .AddIngredient(ItemID.PixieDust, 3)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
	}
}
