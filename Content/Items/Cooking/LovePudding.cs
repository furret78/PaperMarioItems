using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Cooking
{ 
	public class LovePudding : ModItem
	{
		private int buffTime = 3;
		public override void SetDefaults()
		{
			Item.width = 36;
			Item.height = 40;
			Item.useTurn = true;
			Item.useTime = 17;
			Item.useAnimation = Item.useTime;
			Item.useStyle = ItemUseStyleID.EatFood;
			Item.UseSound = SoundID.Item2;
			Item.consumable = true;
			Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(silver: 10);
        }

        public override bool? UseItem(Player player)
        {
            int randChance = Main.rand.Next(0, 3);
			switch (randChance)
			{
				case 0:
					player.AddBuff(BuffID.Invisibility, buffTime*60*60);
                    return true;
                case 1:
					player.AddBuff(PMBuffID.Electrified, buffTime*60*60);
                    return true;
                case 2:
					player.AddBuff(BuffID.Confused, 300);
                    return true;
                default:  return true;
			}
        }
    }
}
