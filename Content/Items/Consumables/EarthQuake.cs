using PaperMarioItems.Common.Players;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class EarthQuake : ModItem
	{
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(PaperPlayer.earthquakeDamage);

        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.ThunderBolt;
            Item.ResearchUnlockCount = 3;
        }

        public override void SetDefaults()
		{
            Item.width = 39;
            Item.height = 37;
            Item.useTurn = true;
            Item.useTime = 17;
            Item.useAnimation = Item.useTime;
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(silver: 15);
        }

        public override bool? UseItem(Player player)
        {
            if (!player.GetModPlayer<PaperPlayer>().causeEarthquake)
            {
                player.GetModPlayer<PaperPlayer>().causeEarthquake = true;
                SoundEngine.PlaySound(PMSoundID.useItem, player.Center);
                return true;
            }
            else return false;
        }
    }
}
