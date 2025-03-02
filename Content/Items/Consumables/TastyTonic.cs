using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class TastyTonic : ModItem
	{
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = PMItemID.SleepySheep;
            ItemID.Sets.DrinkParticleColors[Type] = [new(99, 186, 247)];
            Item.ResearchUnlockCount = 300;
        }

        public override void SetDefaults()
		{
            Item.DefaultToFood(32, 40, 0, 0, true);
            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(silver: 1);
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

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(3)
				.AddIngredient(ItemID.WaterBucket)
                .AddIngredient(RecipeGroupID.Fruit)
				.AddTile(TileID.WorkBenches)
				.Register();
            recipe = CreateRecipe(3)
                .AddIngredient(ItemID.BottledWater, 3)
                .AddIngredient(RecipeGroupID.Fruit)
                .AddTile(TileID.WorkBenches)
                .Register();
            recipe = CreateRecipe(3)
                .AddIngredient(RecipeGroupID.Fruit)
                .AddCondition(Condition.NearWater)
                .AddTile(TileID.Bottles)
                .Register();
        }
	}
}
