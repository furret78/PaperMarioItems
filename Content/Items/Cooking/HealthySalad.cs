using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Cooking
{ 
	public class HealthySalad : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.TurtleyLeaf;
            ItemID.Sets.FoodParticleColors[Type] = [
                new(255, 255, 156),
                new(49, 207, 49),
                new(206, 101, 49)
            ];
            Item.ResearchUnlockCount = 200;
        }

        public override void SetDefaults()
		{
            Item.DefaultToFood(40, 39, 0, 0);
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(silver: 5);
            Item.healMana = 75;
        }

        public override bool? UseItem(Player player)
        {
            bool empty = true;
            for (int l = 0; l < Player.MaxBuffs; l++)
            {
                int num25 = player.buffType[l];
                if (Main.debuff[num25] && player.buffTime[l] > 0 && (num25 < 0 || num25 >= BuffID.Count || !BuffID.Sets.NurseCannotRemoveDebuff[num25]))
                {
                    empty = false;
                    player.DelBuff(l);
                    l = -1;
                }
            }
            if (!empty) SoundEngine.PlaySound(PMSoundID.recover, player.Center);
            return true;
        }


        public override void OnConsumeItem(Player player)
        {
            player.TryToResetHungerToNeutral();
        }
        public override void Load()
        {
            On_Player.ApplyLifeAndOrMana += On_Player_ApplyLifeAndOrMana;
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
                .AddIngredient(RecipeGroupID.Fruit, 10)
                .AddCondition(PaperMarioConditions.HasCookbook)
				.AddTile(TileID.WorkBenches)
				.Register();
        }
	}
}
