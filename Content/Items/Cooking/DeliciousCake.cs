using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Cooking
{ 
	public class DeliciousCake : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.PoisonedCake;
            ItemID.Sets.FoodParticleColors[Type] = [
                Color.White,
                new(255, 231, 148),
                new(156, 44, 41),
                new(255, 166, 0),
                new(255, 211, 74),
                new(165, 0, 0)
            ];
            Item.ResearchUnlockCount = Item.CommonMaxStack;
        }

        public override void SetDefaults()
		{
            Item.DefaultToFood(39, 39, 0, 0);
            Item.rare = ItemRarityID.Expert;
            Item.value = Item.sellPrice(gold: 50);
            Item.healLife = 1;
            Item.healMana = 1;
            Item.potion = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int i = tooltips.FindIndex(line => line.Name == "HealLife");
            if (i != -1) tooltips[i].Hide();
            int j = tooltips.FindIndex(line => line.Name == "HealMana");
            if (j != -1) tooltips[j].Hide();
        }

        public override bool? UseItem(Player player)
        {
            int foodBuff = BuffID.WellFed;
            bool notEmpty = false;
            player.TryToResetHungerToNeutral();
            for (int l = 0; l < Player.MaxBuffs; l++)
            {
                int num25 = player.buffType[l];
                if (Main.debuff[num25] && player.buffTime[l] > 0 && (num25 < 0 || num25 >= BuffID.Count || !BuffID.Sets.NurseCannotRemoveDebuff[num25]))
                {
                    notEmpty = true;
                    player.DelBuff(l);
                    l = -1;
                }
            }
            if (notEmpty) SoundEngine.PlaySound(PMSoundID.recover, player.Center);
            player.AddBuff(BuffID.Regeneration, 36000);
            player.AddBuff(BuffID.ManaRegeneration, 36000);
            if (Main.expertMode) foodBuff = BuffID.WellFed2;
            if (Main.masterMode) foodBuff = BuffID.WellFed3;
            player.AddBuff(foodBuff, 36000);
            return true;
        }

        public override void Load()
        {
            On_Player.ApplyPotionDelay += On_Player_ApplyPotionDelay;
            On_Player.ApplyLifeAndOrMana += On_Player_ApplyLifeAndOrMana;
        }

        private static void On_Player_ApplyPotionDelay(On_Player.orig_ApplyPotionDelay orig, Player player, Item sItem)
        {
            if (sItem.type == PMItemID.DeliciousCake) return;
            else orig(player, sItem);
        }

        private static void On_Player_ApplyLifeAndOrMana(On_Player.orig_ApplyLifeAndOrMana orig, Player player, Item sItem)
        {
            if (sItem.type == PMItemID.DeliciousCake)
            {
                if (player.statLife < player.statLifeMax2)
                {
                    SoundEngine.PlaySound(PMSoundID.fullHeal, player.Center);
                    CombatText.NewText(player.getRect(), CombatText.HealLife, "MAX");
                    player.statLife = player.statLifeMax2;
                }
                if (player.statMana < player.statManaMax2)
                {
                    SoundEngine.PlaySound(PMSoundID.fullMana, player.Center);
                    CombatText.NewText(player.getRect(), CombatText.HealMana, "MAX");
                    player.statMana = player.statManaMax2;
                }
            }
            else orig(player, sItem);
        }
    }
}
