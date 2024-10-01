using PaperMarioItems.Common.Players;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class FireFlower : ModItem
	{
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(PaperPlayer.fireFlowerDamage);

        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.SpicySoup;
        }

        public override void SetDefaults()
		{
            Item.width = 38;
            Item.height = 40;
            Item.useTurn = true;
            Item.useTime = 120;
            Item.useAnimation = Item.useTime;
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.buyPrice(silver: 10);
        }

        public override bool? UseItem(Player player)
        {
            if (player.GetModPlayer<PaperPlayer>().fireFlower <= 0 && !player.GetModPlayer<PaperPlayer>().fireFlowerActive)
            {
                return true;
            }
            else return false;
        }

        public override void OnConsumeItem(Player player)
        {
            SoundEngine.PlaySound(PMSoundID.useItem, player.Center);
            player.GetModPlayer<PaperPlayer>().fireFlower = 26;
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
