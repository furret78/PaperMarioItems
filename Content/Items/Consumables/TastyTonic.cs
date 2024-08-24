using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Map;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class TastyTonic : ModItem
	{
		public override void SetDefaults()
		{
            Item.width = 32;
            Item.height = 40;
            Item.useTurn = true;
            Item.useTime = 17;
            Item.useAnimation = Item.useTime;
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.White;
            Item.value = Item.buyPrice(copper: 25);
        }

        public override bool? UseItem(Player player)
        {
            for (int l = 0; l < Player.MaxBuffs; l++)
            {
                int num25 = player.buffType[l];
                if (Main.debuff[num25] && player.buffTime[l] > 0 && (num25 < 0 || num25 >= BuffID.Count || !BuffID.Sets.NurseCannotRemoveDebuff[num25]))
                {
                    player.DelBuff(l);
                    l = -1;
                }
            }
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
            //cooking
            recipe = CreateRecipe()
                .AddIngredient<HotSauce>()
                .AddIngredient(ItemID.Peach)
                .AddCondition(PaperMarioConditions.HasCookbook)
                .AddTile(TileID.CookingPots)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient<HotSauce>()
                .AddRecipeGroup(Language.GetTextValue($"Mods.PaperMarioItems.Content.SyrupGroup"))
                .AddCondition(PaperMarioConditions.HasCookbook)
                .AddTile(TileID.CookingPots)
                .Register();
        }
	}
}
