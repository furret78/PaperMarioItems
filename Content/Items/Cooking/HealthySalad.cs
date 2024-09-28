using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Cooking
{ 
	public class HealthySalad : ModItem
	{
		public override void SetDefaults()
		{
            Item.width = 40;
            Item.height = 39;
            Item.useTurn = true;
            Item.useTime = 17;
            Item.useAnimation = Item.useTime;
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.UseSound = SoundID.Item2;
            Item.maxStack = Item.CommonMaxStack;
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
