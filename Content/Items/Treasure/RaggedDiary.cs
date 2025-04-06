using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Treasure
{ 
	public class RaggedDiary : ModItem
	{
		public static LocalizedText DiaryDeath { get; set; }

        public override void SetStaticDefaults()
        {
            DiaryDeath = this.GetLocalization(nameof(DiaryDeath));
        }

        public override void SetDefaults()
		{
			Item.width = 38;
			Item.height = 32;
			Item.value = Item.sellPrice(silver: 2);
			Item.rare = ItemRarityID.Blue;
			Item.consumable = true;
		}

        public override bool CanRightClick() => true;

        public override void RightClick(Player player)
        {
            int numberOfDiaries = player.CountItem(PMItemID.RaggedDiary, 9999);
            for (int i = 0; i < numberOfDiaries + 1; i++)
            {
                player.ConsumeItem(PMItemID.RaggedDiary);
            }
            NetworkText diaryDeathMessage = NetworkText.FromKey(DiaryDeath.Key, player.name);
            player.KillMe(damageSource: PlayerDeathReason.ByCustomReason(diaryDeathMessage), 10, 1);
        }
    }
}
