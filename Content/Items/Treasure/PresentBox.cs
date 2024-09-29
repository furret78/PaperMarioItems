using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Treasure
{ 
	public class PresentBox : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 38;
			Item.height = 40;
			Item.noUseGraphic = true;
			Item.consumable = true;
			Item.value = Item.sellPrice(silver: 70);
			Item.rare = ItemRarityID.Pink;
		}

        public override bool CanRightClick() => true;

        public override void RightClick(Player player)
        {
            player.QuickSpawnItem(player.GetSource_ItemUse(Item), ChooseItem());
            player.QuickSpawnItem(player.GetSource_ItemUse(Item), PMItemID.PresentPaper);
        }

		private int ChooseItem()
		{
			int resultItem;
            switch (Main.rand.Next(3))
            {
                case 0:
                    {
                        resultItem = PMItemID.MushroomCake;
                        break;
                    }
                case 1:
                    {
                        resultItem = ItemID.Mango;
                        break;
                    }
                case 2:
                    {
                        resultItem = PMItemID.FrightMask;
                        break;
                    }
                default:
                    {
                        resultItem = ItemID.GoldCoin;
                        break;
                    }
            }
            return resultItem;
		}
    }
}
