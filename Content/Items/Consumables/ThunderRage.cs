using PaperMarioItems.Common.Players;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{
	public class ThunderRage : ModItem
	{
        public override void SetDefaults()
		{
            Item.width = 28;
            Item.height = 40;
            Item.useTurn = true;
            Item.useTime = 17;
            Item.useAnimation = Item.useTime;
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.White;
            Item.value = Item.buyPrice(copper: 25);
        }

        public override bool? UseItem(Player player)
        {
            if (!player.GetModPlayer<PaperPlayer>().thunderEffectActive && !player.GetModPlayer<PaperPlayer>().thunderAll)
            {
                player.GetModPlayer<PaperPlayer>().thunderAll = true;
                SoundEngine.PlaySound(PaperMarioItems.useItemPM);
                return true;
            }
            else return false;
        }
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe()
                .AddIngredient(ItemID.RainCloud, 30)
                .AddIngredient(ItemID.SoulofFlight, 8)
				.AddTile(TileID.WorkBenches)
				.Register();
            recipe = CreateRecipe()
                .AddIngredient(ModContent.ItemType<ThunderBolt>(), 3)
                .AddIngredient(ItemID.SoulofFlight, 5)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
	}
}
