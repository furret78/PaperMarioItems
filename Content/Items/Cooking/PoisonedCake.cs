using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Cooking
{ 
	public class PoisonedCake : ModItem
    {
        private const int buffTime = 216000;

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
            player.AddBuff(BuffID.Poisoned, buffTime);
            player.AddBuff(BuffID.Darkness, buffTime);
            player.AddBuff(BuffID.Cursed, buffTime);
            player.AddBuff(BuffID.OnFire, buffTime);
            player.AddBuff(BuffID.Bleeding, buffTime);
            player.AddBuff(BuffID.Confused, buffTime);
            player.AddBuff(BuffID.Slow, buffTime);
            player.AddBuff(BuffID.Weak, buffTime);
            player.AddBuff(BuffID.Silenced, buffTime);
            player.AddBuff(BuffID.BrokenArmor, buffTime);
            player.AddBuff(BuffID.Suffocation, buffTime);
            player.AddBuff(BuffID.ManaSickness, buffTime);
            player.AddBuff(BuffID.PotionSickness, buffTime / 60);
            player.AddBuff(BuffID.Chilled, buffTime);
            player.AddBuff(BuffID.Frostburn, buffTime);
            player.AddBuff(BuffID.Frostburn2, buffTime);
            player.AddBuff(BuffID.Electrified, buffTime);
            player.AddBuff(BuffID.Blackout, buffTime);
            player.AddBuff(PMBuffID.Dizzy, buffTime);
            player.AddBuff(PMBuffID.Soft, buffTime);
            return true;
        }
	}
}