using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.CookingSPM
{ 
	public class EmergencyRation : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.MushroomShake;
            Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
		{
            Item.DefaultToFood(34, 39, BuffID.WellFed, 7200);
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(silver: 25);
            Item.healLife = 50;
            Item.potion = true;
        }

        public override void OnConsumeItem(Player player)
		{
            player.TryToResetHungerToNeutral();
            player.ClearBuff(BuffID.Poisoned);
        }

        public override void Load()
        {
            On_Player.ApplyPotionDelay += On_Player_ApplyPotionDelay;
            On_Player.ApplyLifeAndOrMana += On_Player_ApplyLifeAndOrMana;
        }

        private static void On_Player_ApplyPotionDelay(On_Player.orig_ApplyPotionDelay orig, Player player, Item sItem)
        {
            if (sItem.type == PMItemID.EmergencyRation) return;
            else orig(player, sItem);
        }
        private static void On_Player_ApplyLifeAndOrMana(On_Player.orig_ApplyLifeAndOrMana orig, Player player, Item sItem)
        {
            if (sItem.type == PMItemID.EmergencyRation)
            {
                int num = sItem.healLife;

                if (player.ZoneUnderworldHeight ||
                    Main.npc.Take(Main.maxNPCs).Any(n => n.active && n.boss))
                    num += 50;

                player.statLife += num;
                if (player.statLife > player.statLifeMax2) player.statLife = player.statLifeMax2;
                if (num > 0 && Main.myPlayer == player.whoAmI) player.HealEffect(num);
            }
            else orig(player, sItem);
        }
    }
}
