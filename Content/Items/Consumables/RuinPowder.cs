using PaperMarioItems.Common.Players;
using PaperMarioItems.Content.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{
	public class RuinPowder : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 39;
			Item.height = 40;
			Item.useTurn = true;
			Item.useTime = 17;
			Item.useAnimation = Item.useTime;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.consumable = true;
			Item.maxStack = Item.CommonMaxStack;
			Item.rare = ItemRarityID.Orange;
			Item.value = Item.buyPrice(copper: 25);
		}

        public override bool? UseItem(Player player)
        {
			if (!player.GetModPlayer<PaperPlayer>().ruinPowderActive && player.GetModPlayer<PaperPlayer>().ruinPowderCooldown <= 0)
			{
				player.GetModPlayer<PaperPlayer>().ruinPowderActive = true;
				player.GetModPlayer<PaperPlayer>().ruinPowderCooldown = Item.useTime * 2 + 1;
                return true;
			}
			return false;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe()
				.AddIngredient(ItemID.LeadOre, 5)
				.AddIngredient(ItemID.VilePowder)
				.AddTile(TileID.WorkBenches)
				.Register();
            recipe = CreateRecipe()
                .AddIngredient(ItemID.LeadOre, 5)
                .AddIngredient(ItemID.ViciousPowder)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
	}
}
