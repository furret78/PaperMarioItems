using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class GradualSyrup : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 40;
			Item.useTurn = true;
			Item.useTime = 17;
			Item.useAnimation = Item.useTime;
			Item.useStyle = ItemUseStyleID.EatFood;
			Item.UseSound = SoundID.Item2;
			Item.consumable = true;
			Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(copper: 25);
			Item.buffType = BuffID.ManaRegeneration;
			Item.buffTime = 36000;
        }
        public override void OnConsumeItem(Player player)
        {
            player.TryToResetHungerToNeutral();
        }
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(2)
				.AddIngredient<HoneySyrup>(2)
                .AddIngredient(ItemID.ManaRegenerationPotion)
                .Register();
            recipe = CreateRecipe(3)
                .AddIngredient<MapleSyrup>()
                .AddIngredient(ItemID.ManaRegenerationPotion)
                .Register();
            recipe = CreateRecipe(5)
                .AddIngredient<JamminJelly>()
                .AddIngredient(ItemID.ManaRegenerationPotion)
                .Register();
        }
	}
}
