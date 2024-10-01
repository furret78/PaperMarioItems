using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Cooking
{ 
	public class PeachTart : ModItem
	{
		private const int buffTime = 3;

        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.CakeMix;
            ItemID.Sets.FoodParticleColors[Type] = [
                new(255, 219, 222),
                new(255, 182, 189),
                new(198, 65, 90),
                new(231, 190, 140),
                new(198, 142, 74)
            ];
            Item.ResearchUnlockCount = 25;
        }

        public override void SetDefaults()
		{
            Item.DefaultToFood(38, 32, 0, 0);
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.buyPrice(silver: 10);
        }

        public override bool? UseItem(Player player)
        {
            int randChance = Main.rand.Next(0, 3);
			switch (randChance)
			{
				case 0:
					player.AddBuff(PMBuffID.Dodgy, buffTime*60*60);
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
