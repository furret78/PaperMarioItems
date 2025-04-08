using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Cooking
{ 
	public class PoisonedCake : ModItem
    {
        private const int buffTime = 216000;

        private static List<int> listOfAilments =
            [BuffID.Poisoned, BuffID.Darkness, BuffID.Cursed, BuffID.OnFire, BuffID.Bleeding, BuffID.Confused,
            BuffID.Slow, BuffID.Weak, BuffID.Silenced, BuffID.BrokenArmor, BuffID.Suffocation, BuffID.ManaSickness, BuffID.Chilled,
            BuffID.Frostburn2, BuffID.Electrified, BuffID.Blackout, PMBuffID.Dizzy, PMBuffID.Soft];

        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.DeliciousCake;
            ItemID.Sets.FoodParticleColors[Type] = [
                new(165, 0, 0),
                new(255, 199, 198),
                new(255, 166, 0)
            ];
            Item.ResearchUnlockCount = 200;
        }

        public override void SetDefaults()
		{
            Item.DefaultToFood(39, 39, 0, 0);
            Item.rare = ItemRarityID.Expert;
            Item.value = Item.sellPrice(silver: 1);
        }

        public override bool? UseItem(Player player)
        {
            player.AddBuff(BuffID.PotionSickness, buffTime / 60);
            for (int i = 0; i < listOfAilments.Count; i++)
            {
                player.AddBuff(listOfAilments[i], buffTime);
            }
            return true;
        }
	}
}