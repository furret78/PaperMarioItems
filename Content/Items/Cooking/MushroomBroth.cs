 using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace PaperMarioItems.Content.Items.Cooking
{ 
	public class MushroomBroth : ModItem
	{
        public const int healTime = 5;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(healTime);

        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.SlowMushroom;
            ItemID.Sets.DrinkParticleColors[Type] = [new(198, 105, 74)];
            ItemID.Sets.FoodParticleColors[Type] = [new(74, 52, 0), new(214, 166, 82)];
            Item.ResearchUnlockCount = 50;
        }

        public override void SetDefaults()
		{
            Item.DefaultToFood(34, 39, BuffID.WellFed, 3600, true);
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(silver: 26);
        }

        public override void OnConsumeItem(Player player)
		{
            player.AddBuff(BuffID.Regeneration, healTime*60*60);
            player.AddBuff(PMBuffID.Soft, healTime*60*60);
            player.TryToResetHungerToNeutral();
        }

        public override void Load()
        {
            On_Player.ApplyLifeAndOrMana += On_Player_ApplyLifeAndOrMana;
        }

        private static void On_Player_ApplyLifeAndOrMana(On_Player.orig_ApplyLifeAndOrMana orig, Player player, Item sItem)
        {
            if (sItem.type == PMItemID.MushroomBroth)
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
