using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.ConsumablesSPM
{ 
	public class SapSoup : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.FoodParticleColors[Type] = [
                new(255, 231, 107),
                new(255, 247, 214)
            ];
            Item.ResearchUnlockCount = 280;
        }

        public override void SetDefaults()
		{
            Item.DefaultToFood(40, 37, BuffID.WellFed, 3600, true);
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 8);
            Item.healLife = 30;
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
        }

        private void On_Player_ApplyPotionDelay(On_Player.orig_ApplyPotionDelay orig, Player player, Item sItem)
        {
            if (sItem.type == Type) return;
            else orig(player, sItem);
        }
    }
}
