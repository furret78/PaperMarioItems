using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Cooking
{ 
	public class PoisonedCake : ModItem
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
            Item.rare = ItemRarityID.Expert;
            Item.value = Item.sellPrice(silver: 1);
        }
        public override bool? UseItem(Player player)
        {
            player.AddBuff(BuffID.Poisoned, 216000);
            player.AddBuff(BuffID.Darkness, 216000);
            player.AddBuff(BuffID.Cursed, 216000);
            player.AddBuff(BuffID.OnFire, 216000);
            player.AddBuff(BuffID.Bleeding, 216000);
            player.AddBuff(BuffID.Confused, 216000);
            player.AddBuff(BuffID.Slow, 216000);
            player.AddBuff(BuffID.Weak, 216000);
            player.AddBuff(BuffID.Silenced, 216000);
            player.AddBuff(BuffID.BrokenArmor, 216000);
            player.AddBuff(BuffID.Suffocation, 216000);
            player.AddBuff(BuffID.ManaSickness, 216000);
            player.AddBuff(BuffID.PotionSickness, 216000);
            player.AddBuff(BuffID.Chilled, 216000);
            player.AddBuff(BuffID.Frostburn, 216000);
            player.AddBuff(BuffID.Frostburn2, 216000);
            player.AddBuff(BuffID.Electrified, 216000);
            player.AddBuff(BuffID.Blackout, 216000);
            player.AddBuff(PMBuffID.Dizzy, 216000);
            player.AddBuff(PMBuffID.Soft, 216000);
            return true;
        }
	}
}