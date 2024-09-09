using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Cooking
{ 
	public class DeliciousCake : ModItem
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
            Item.value = Item.sellPrice(gold: 50);
            Item.expertOnly = true;
        }
        public override bool? UseItem(Player player)
        {
            int missingHealth = player.statLifeMax2 - player.statLife;
            int missingMana = player.statManaMax2 - player.statMana;
            player.Heal(missingHealth);
            player.statMana = player.statManaMax2;
            player.ManaEffect(missingMana);
            player.TryToResetHungerToNeutral();
            for (int l = 0; l < Player.MaxBuffs; l++)
            {
                int num25 = player.buffType[l];
                if (Main.debuff[num25] && player.buffTime[l] > 0 && (num25 < 0 || num25 >= BuffID.Count || !BuffID.Sets.NurseCannotRemoveDebuff[num25]))
                {
                    player.DelBuff(l);
                    l = -1;
                }
            }
            player.AddBuff(BuffID.Regeneration, 36000);
            player.AddBuff(BuffID.ManaRegeneration, 36000);
            player.AddBuff(BuffID.WellFed3, 36000);
            return true;
        }
	}
}
