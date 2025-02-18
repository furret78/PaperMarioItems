using PaperMarioItems.Common.Players;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class LifeMushroom : ModItem
	{
        public const int customPotionTime = 5;
        public static LocalizedText RestoreText { get; private set; }
        public override void SetStaticDefaults()
        {
            RestoreText = this.GetLocalization(nameof(RestoreText));
            Item.ResearchUnlockCount = 10;
            ItemID.Sets.FoodParticleColors[Type] = [
                new(231, 227, 231),
                new(231, 56, 57),
                new(255, 251, 132)
            ];
        }
        public override void SetDefaults()
        {
            Item.DefaultToFood(40, 40, 0, 0);
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.buyPrice(silver: 30);
            Item.healLife = 10;
            Item.potion = true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine line = tooltips.FirstOrDefault(x => x.Mod == "Terraria" && x.Name == "HealLife");
            if (line != null)
            {
                line.Text = Language.GetTextValue(RestoreText.Format(Item.healLife, PaperPlayer.postReviveProtect, PaperPlayer.postReviveRegen, PaperPlayer.postReviveHeal));
            }
        }
        public override void OnConsumeItem(Player player)
        {
            player.TryToResetHungerToNeutral();
        }
        //detour
        public override void Load()
        {
            On_Player.ApplyPotionDelay += On_Player_ApplyPotionDelay;
        }
        private void On_Player_ApplyPotionDelay(On_Player.orig_ApplyPotionDelay orig, Player self, Item sItem)
        {
            if (sItem.type == Type)
            {
                int delay = customPotionTime * 60;
                if (self.pStone) delay = (int)(delay * Player.PhilosopherStoneDurationMultiplier);
                //do not apply the 5-sec cooldown if revived by life shroom
                if (self.GetModPlayer<PaperPlayer>().lifeShroomRevive)
                    self.GetModPlayer<PaperPlayer>().lifeShroomRevive = false;
                else self.AddBuff(21, delay);
            }
            else orig(self, sItem);
        }
        public override void AddRecipes()
		{
            //with life crystals
            Recipe recipe = CreateRecipe()
                .AddRecipeGroup(nameof(ItemID.Mushroom), 10)
                .AddIngredient(ItemID.LifeCrystal, 5)
                .AddTile(TileID.WorkBenches)
				.Register();
            recipe = CreateRecipe()
                .AddIngredient(PMItemID.Mushroom, 5)
                .AddIngredient(ItemID.LifeCrystal, 3)
                .AddTile(TileID.WorkBenches)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient(PMItemID.SuperMushroom, 3)
                .AddIngredient(ItemID.LifeCrystal, 2)
                .AddTile(TileID.WorkBenches)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient(PMItemID.UltraMushroom)
                .AddIngredient(ItemID.LifeCrystal)
                .AddTile(TileID.WorkBenches)
                .Register();
            //with life fruit
            recipe = CreateRecipe()
                .AddRecipeGroup(nameof(ItemID.Mushroom), 8)
                .AddIngredient(ItemID.LifeFruit)
                .AddTile(TileID.WorkBenches)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient(PMItemID.Mushroom, 4)
                .AddIngredient(ItemID.LifeFruit)
                .AddTile(TileID.WorkBenches)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient(PMItemID.SuperMushroom, 2)
                .AddIngredient(ItemID.LifeFruit)
                .AddTile(TileID.WorkBenches)
                .Register();
            recipe = CreateRecipe()
                .AddIngredient(PMItemID.UltraMushroom)
                .AddIngredient(ItemID.LifeFruit)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
