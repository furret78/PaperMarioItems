using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using PaperMarioItems.Content.Items.Consumables;
using PaperMarioItems.Common.Players;

namespace PaperMarioItems.Content.Items.Cooking
{ 
	public class HottestDog : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.FoodParticleColors[Type] = [
                new(158, 0, 27),
                new(255, 66, 0),
                new(178, 73, 31),
                new(96, 16, 0)
            ];
            Item.ResearchUnlockCount = 10;
        }
        public override void SetDefaults()
		{
            Item.DefaultToFood(40, 40, BuffID.WellFed3, 43200);
            Item.rare = ItemRarityID.Orange;
            Item.value = ContentSamples.ItemsByType[ItemID.Hotdog].value + Item.sellPrice(silver: 5);
            Item.healLife = 70;
            Item.healMana = Item.healLife;
            Item.potion = true;
        }

        public override void OnConsumeItem(Player player)
		{
            player.GetModPlayer<PaperPlayer>().DrinkHotSauce(player);
            player.TryToResetHungerToNeutral();
        }

        public override void Load()
        {
            On_Player.ApplyPotionDelay += On_Player_ApplyPotionDelay;
            On_Player.ApplyLifeAndOrMana += On_Player_ApplyLifeAndOrMana;
        }

        private void On_Player_ApplyPotionDelay(On_Player.orig_ApplyPotionDelay orig, Player player, Item sItem)
        {
            if (sItem.type == Type) return;
            else orig(player, sItem);
        }
        private void On_Player_ApplyLifeAndOrMana(On_Player.orig_ApplyLifeAndOrMana orig, Player player, Item sItem)
        {
            if (sItem.type == Type)
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
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe()
				.AddIngredient(ItemID.Hotdog)
                .AddIngredient<HotSauce>()
                .AddCondition(PaperMarioConditions.HasCookbook)
                .AddTile(TileID.WorkBenches)
				.Register();
        }
	}
}
