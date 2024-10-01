using Microsoft.Xna.Framework;
using PaperMarioItems.Common.Players;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{
	public class ThunderBolt : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.EarthQuake;
            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults()
		{
            Item.width = 38;
            Item.height = 39;
            Item.useTurn = true;
            Item.useTime = 17;
            Item.useAnimation = Item.useTime;
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(silver: 4);
        }

        public override bool? UseItem(Player player)
        {
            if (!player.GetModPlayer<PaperPlayer>().thunderEffectActive && !player.GetModPlayer<PaperPlayer>().thunderOnce)
            {
                player.GetModPlayer<PaperPlayer>().thunderOnce = true;
                SoundEngine.PlaySound(PMSoundID.useItem, player.Center);
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
                .AddIngredient(ItemID.RainCloud, 10)
				.AddTile(TileID.WorkBenches)
				.Register();
        }
	}
}
