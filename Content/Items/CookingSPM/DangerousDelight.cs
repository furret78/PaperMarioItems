using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.CookingSPM
{
	public class DangerousDelight : ModItem
	{
        private const int effectTime = 60 * 60;

        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.PoisonMushroom;
            ItemID.Sets.FoodParticleColors[Type] = [
                Color.Cyan,
                new(156, 206, 99),
                new(231, 165, 33),
                new(156, 99, 140),
                Color.Brown
            ];
            Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
		{
            Item.width = 40;
            Item.height = 40;
            Item.useTurn = true;
            Item.useTime = 17;
            Item.useAnimation = Item.useTime;
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.maxStack = Item.CommonMaxStack;
            Item.UseSound = SoundID.Item2;
            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(silver: 3);
            Item.buffType = BuffID.Slow;
            Item.buffTime = effectTime;
        }

        public override void OnConsumeItem(Player player)
        {
            player.AddBuff(BuffID.Confused, effectTime);
            player.AddBuff(BuffID.Silenced, effectTime);
        }
    }
}
