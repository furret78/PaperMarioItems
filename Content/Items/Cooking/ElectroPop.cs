using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Cooking
{ 
	public class ElectroPop : ModItem
	{
        private int effectTime = 3;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(effectTime);
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.VoltMushroom;
            ItemID.Sets.FoodParticleColors[Type] = [
                new(255, 195, 0),
                new(99, 101, 99),
                new(49, 48, 49)
            ];
            Item.ResearchUnlockCount = 30;
        }
        public override void SetDefaults()
		{
            Item.DefaultToFood(40, 39, BuffID.WellFed, 3600);
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(silver: 25);
            Item.healMana = 75;
        }
        public override void OnConsumeItem(Player player)
		{
            player.AddBuff(PMBuffID.Electrified, effectTime*60*60);
            player.TryToResetHungerToNeutral();
        }
        public override void Load()
        {
            On_Player.ApplyLifeAndOrMana += On_Player_ApplyLifeAndOrMana;
        }

        private static void On_Player_ApplyLifeAndOrMana(On_Player.orig_ApplyLifeAndOrMana orig, Player player, Item sItem)
        {
            if (sItem.type == PMItemID.ElectroPop)
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

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.LightYellow.ToVector3() * 0.55f * Main.essScale);
        }
    }
}
