using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Treasure
{
    [AutoloadEquip(EquipType.Legs)]
    public class BattleTrunks : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 39;
			Item.maxStack = Item.CommonMaxStack;
			Item.rare = ItemRarityID.Gray;
			Item.accessory = true;
		}

        public override void UpdateEquip(Player player)
        {
            if (!player.HasBuff(BuffID.Stinky)) player.AddBuff(BuffID.Stinky, 60);
        }

        public override void UpdateVanity(Player player)
        {
            if (!player.HasBuff(BuffID.Stinky)) player.AddBuff(BuffID.Stinky, 60);
        }
    }
}
