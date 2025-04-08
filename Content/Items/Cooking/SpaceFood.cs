using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Cooking
{ 
	public class SpaceFood : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.DriedBouquet;
            ItemID.Sets.FoodParticleColors[Type] = [
                new(208, 150, 41),
                new(255, 186, 49),
                new(140, 101, 24)
            ];
            Item.ResearchUnlockCount = 300;
        }

        public override void SetDefaults()
		{
            Item.DefaultToFood(35, 28, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(silver: 10);
            Item.healLife = 25;
            Item.potion = true;
        }

        public override bool? UseItem(Player player)
        {
            if (Main.rand.NextBool(2)) player.AddBuff(PMBuffID.Allergic, 18000);
            return true;
        }

        public override void Load()
        {
            On_Player.ApplyPotionDelay += On_Player_ApplyPotionDelay;
        }

        private static void On_Player_ApplyPotionDelay(On_Player.orig_ApplyPotionDelay orig, Player self, Item sItem)
        {
            if (sItem.type == PMItemID.SpaceFood) return;
            else orig(self, sItem);
        }
    }
}
