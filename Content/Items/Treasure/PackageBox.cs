using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Treasure
{
	public class PackageBox : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 39;
			Item.height = 33;
			Item.maxStack = Item.CommonMaxStack;
			Item.value = Item.sellPrice(copper: 5);
			Item.rare = ItemRarityID.White;
			Item.noUseGraphic = true;
		}

		public override bool CanRightClick() => true;

		public override void RightClick(Player player)
		{
			int itemToSpawn = RandomImportantThing(player);
            player.QuickSpawnItem(player.GetSource_ItemUse(Item), itemToSpawn);
        }

		private int RandomImportantThing(Player player)
		{
			int resultItem = PMItemID.BattleTrunks;
			for (int i = 0; i < 1000; i++)
			{
				resultItem = ImportantThings.ImportantThingsList[Main.rand.Next(ImportantThings.ImportantThingsList.Count)];
				if (resultItem == PMItemID.PackageBox || resultItem == PMItemID.LotteryTicket)
				{
					return PMItemID.BattleTrunks;
				}
				else if (player.HasItemInAnyInventory(resultItem)) continue;
				else return resultItem;
			}
			return resultItem;
		}
	}
}