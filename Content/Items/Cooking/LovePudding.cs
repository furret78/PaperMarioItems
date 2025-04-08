using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Cooking
{ 
	public class LovePudding : ModItem
	{
		private const int buffTime = 3;

        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.MysticEgg;
            ItemID.Sets.FoodParticleColors[Type] = [
                new(255, 219, 24),
                new(255, 170, 0),
                new(255, 255, 206)
            ];
            Item.ResearchUnlockCount = 40;
        }

        public override void SetDefaults()
		{
            Item.DefaultToFood(36, 40, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(silver: 10);
        }

        public override bool? UseItem(Player player)
        {
            int randChance = Main.rand.Next(0, 3);
			switch (randChance)
			{
				case 0:
					player.AddBuff(BuffID.Invisibility, buffTime*60*60);
					if (!player.HasBuff(PMBuffID.Allergic) && !player.buffImmune[BuffID.Invisibility]) SoundEngine.PlaySound(PMSoundID.invisible, player.Center);
                    return true;
                case 1:
					player.AddBuff(PMBuffID.Electrified, buffTime*60*60);
                    return true;
                case 2:
					player.AddBuff(BuffID.Confused, 300);
                    return true;
                default:  return true;
			}
        }
    }
}
