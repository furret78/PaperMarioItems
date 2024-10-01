using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Cooking
{ 
	public class MeteorMeal : ModItem
	{
        private const int effectTime = 2;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(effectTime);
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.ShootingStar;
            ItemID.Sets.FoodParticleColors[Type] = [
                new(239, 182, 90),
                new(255, 195, 0),
                new(189, 134, 66)
            ];
            Item.ResearchUnlockCount = 50;
        }
        public override void SetDefaults()
		{
            Item.DefaultToFood(40, 40, BuffID.WellFed, 3600);
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(silver: 30);
            Item.healMana = 35;
        }
        public override void OnConsumeItem(Player player)
		{
            player.AddBuff(BuffID.Regeneration, effectTime*60*60);
            player.TryToResetHungerToNeutral();
        }
        public override void Load()
        {
            On_Player.ApplyLifeAndOrMana += On_Player_ApplyLifeAndOrMana;
        }
        private void On_Player_ApplyLifeAndOrMana(On_Player.orig_ApplyLifeAndOrMana orig, Player player, Item sItem)
        {
            if (sItem.type == Type)
            {
                int num = sItem.healLife; int num2 = sItem.healMana;
                player.statLife += num; player.statMana += num2;
                if (player.statLife > player.statLifeMax2) player.statLife = player.statLifeMax2;
                if (player.statMana > player.statManaMax2) player.statMana = player.statManaMax2;
                if (num > 0 && Main.myPlayer == player.whoAmI) player.HealEffect(num);
                if (num2 > 0)
                {
                    if (Main.myPlayer == player.whoAmI) player.ManaEffect(num2);
                }
            }
            else orig(player, sItem);
        }
	}
}
