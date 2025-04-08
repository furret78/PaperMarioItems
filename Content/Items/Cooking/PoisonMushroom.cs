using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Cooking
{ 
	public class PoisonMushroom : ModItem
    {
        public static LocalizedText PoisonDeath { get; private set; }

        public override void SetStaticDefaults()
        {
            PoisonDeath = this.GetLocalization(nameof(PoisonDeath));
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.SlowMushroom;
            ItemID.Sets.FoodParticleColors[Type] = [
                new(57, 101, 41),
                new(198, 125, 148),
                new(148, 170, 140)
            ];
            Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
		{
			Item.DefaultToFood(36, 39, 0, 0);
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(silver: 20);
        }

        public override bool? UseItem(Player player)
        {
			if (Main.rand.Next(0, 15) == 1)
			{
				int healAmount = player.statLifeMax2 - player.statLife;
				player.statLife += healAmount;
				player.HealEffect(healAmount);
			}
			else
			{
				player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " " + PoisonDeath), (player.statLife / 2), player.direction * (-1), false, false, -1, false, player.statDefense);
				if (!player.HasBuff(PMBuffID.Allergic) && !player.buffImmune[BuffID.Poisoned])
				{
					SoundEngine.PlaySound(PMSoundID.causeStatus, player.Center);
				}
                player.AddBuff(BuffID.Poisoned, 3600);
            }
			return true;
        }
	}
}
