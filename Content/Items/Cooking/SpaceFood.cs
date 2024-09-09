using PaperMarioItems.Common.Players;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.Biomes;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Cooking
{ 
	public class SpaceFood : ModItem
	{
        public override void SetDefaults()
		{
			Item.width = 35;
			Item.height = 28;
			Item.useTurn = true;
			Item.useTime = 17;
			Item.useAnimation = Item.useTime;
			Item.useStyle = ItemUseStyleID.EatFood;
			Item.UseSound = SoundID.Item2;
			Item.consumable = true;
			Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(silver: 10);
            Item.buffType = PMBuffID.Allergic;
            Item.buffTime = 18000;
            Item.healLife = 25;
            Item.potion = true;
        }

        public override bool? UseItem(Player player)
        {
            if (player.ZoneNormalSpace) player.Heal(25);
            return true;
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
